using Microsoft.Extensions.Options;
using ShipperStation.Application.Contracts.Payments;
using ShipperStation.Application.Interfaces.Services.Payments;
using ShipperStation.Infrastructure.Settings;
using ShipperStation.Shared.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace ShipperStation.Infrastructure.Services.Payments;

public class MomoPaymentService : IMomoPaymentService
{
    private const string DefaultOrderInfo = "[SStation] Thanh toán với Momo";

    private readonly MomoSettings _momoSettings;
    public MomoPaymentService(IOptions<MomoSettings> momoSettings)
    {
        _momoSettings = momoSettings.Value;
    }

    public async Task<string> CreatePayment(MomoPayment payment)
    {
        var requestType = "captureWallet";
        var request = new MomoPaymentRequest();
        request.OrderInfo = payment.Info ?? DefaultOrderInfo;
        request.PartnerCode = _momoSettings.PartnerCode;
        request.IpnUrl = $"{_momoSettings.IpnUrl}/{payment.PaymentReferenceId}";
        request.RedirectUrl = $"{_momoSettings.RedirectUrl}/{payment.PaymentReferenceId}";
        request.Amount = payment.Amount;
        request.OrderId = payment.PaymentReferenceId;
        request.ReferenceId = $"{payment.PaymentReferenceId}";
        request.RequestId = Guid.NewGuid().ToString();
        request.RequestType = requestType;
        request.ExtraData = string.Empty;
        request.AutoCapture = true;
        request.Lang = "vi";

        var rawSignature = $"accessKey={_momoSettings.AccessKey}&amount={request.Amount}&extraData={request.ExtraData}&ipnUrl={request.IpnUrl}&orderId={request.OrderId}&orderInfo={request.OrderInfo}&partnerCode={request.PartnerCode}&redirectUrl={request.RedirectUrl}&requestId={request.RequestId}&requestType={requestType}";
        request.Signature = GetSignature(rawSignature, _momoSettings.SecretKey);

        var httpContent = new StringContent(JsonSerializerUtils.Serialize(request), Encoding.UTF8, "application/json");
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(30);
        var momoResponse = await httpClient.PostAsync(_momoSettings.PaymentEndpoint, httpContent);
        var responseContent = momoResponse.Content.ReadAsStringAsync().Result;

        if (momoResponse.IsSuccessStatusCode)
        {
            var momoPaymentResponse = JsonSerializerUtils.Deserialize<MomoPaymentResponse>(responseContent);
            if (momoPaymentResponse != null)
            {
                //var pm = new Payment()
                //{
                //    Method = PaymentMethod.Momo,
                //    Amount = momoPaymentResponse.Amount,
                //    // Test with QR
                //    Qr = momoPaymentResponse.PayUrl,
                //    TransactionUrl = momoPaymentResponse.PayUrl,
                //    Deeplink = momoPaymentResponse.Deeplink,
                //    //ReferenceId = payment.PaymentReferenceId,
                //    Content = payment.Info,
                //    Status = PaymentStatus.Created,
                //    Description = "Payment with momo"
                //};

                //return pm;

                return momoPaymentResponse.PayUrl;
            }
        }

        throw new Exception($"[Momo payment] Error: There is some error when create payment with momo. {responseContent}");
    }

    private static string GetSignature(string text, string key)
    {
        // change according to your needs, an UTF8Encoding
        // could be more suitable in certain situations
        var encoding = new UTF8Encoding();

        var textBytes = encoding.GetBytes(text);
        var keyBytes = encoding.GetBytes(key);

        byte[] hashBytes;

        using HMACSHA256 hash = new HMACSHA256(keyBytes);
        hashBytes = hash.ComputeHash(textBytes);

        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}
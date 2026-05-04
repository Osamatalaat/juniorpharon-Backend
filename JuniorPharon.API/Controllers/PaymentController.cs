using Igor.Gateway.Dtos.WebHooks;
using JuniorPharon.Repository;
using JuniorPharon.Services;
using JuniorPharon.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JuniorPharon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        //[HttpPost("payment/webhook")]
        //public async Task<IActionResult> PaymentWebhook([FromBody] WebHookDto dto)
        //{
        //    // تحقق من الدفع، حدِّث Payment.IsDone = true
        //    var payment = await _unitOfWork._paymentRepository.GetByTransactionIdAsync(dto.TransactionId);
        //    if (payment != null)
        //    {
        //        payment.IsDone = true;
        //        await _unitOfWork.SaveChangesAsync();
        //    }
        //    return Ok();
        //}

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment(PaymentCreateVM vm)
        {
            var result = await _paymentService.CreatePaymentAsync(vm);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

    }
}

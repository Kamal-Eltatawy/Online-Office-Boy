using System;

namespace ApplicationService.ServicesHelper
{
    public static class Helper
    {
        public static string GenerateOrderNumber(int orderId)
        {
            string currentDate = DateTime.Now.ToString("yyyyMMdd");

            string paddedOrderId = orderId.ToString().PadLeft(2, '0');

            string orderNumber = currentDate + paddedOrderId;

            return orderNumber;
        }
    }
}

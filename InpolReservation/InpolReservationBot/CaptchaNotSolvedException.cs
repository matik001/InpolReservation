using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InpolReservation.InpolReservationBot
{
    internal class CaptchaNotSolvedException : Exception
    {
        public CaptchaNotSolvedException() {
        }

        public CaptchaNotSolvedException(string message) : base(message) {
        }

        public CaptchaNotSolvedException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}

using System;

namespace congestion.calculator.DTOs
{
    public class TollFeeDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Value as Minute
        /// ex : Clock 6:30 =&gt; (6 * 60) + 30 = 390
        /// </summary>
        public int StartTimeOfDay { get; set; }
        public int Fee { get; set; }
    }
}

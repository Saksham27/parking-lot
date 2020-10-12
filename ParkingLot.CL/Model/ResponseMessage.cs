using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ParkingLot.CL.Model
{
  
        /// <summary>
        /// Poco class for Response message
        /// </summary>
        #region ResponseMessage
        [DataContract]
        public class ResponseMessage<T>
        {
            /// <summary>
            /// Gets or sets Status
            /// </summary>
            [DataMember(Name = "Status")]
            public bool Status { get; set; }

            /// <summary>
            /// Gets or sets MessageC:\Users\Saksham\source\repos\ParkingLot\ParkingLot.CL\Model\ResponseMessage.cs
            /// </summary>
            [DataMember(Name = "Message")]
            public string Message { get; set; }

            /// <summary>
            /// Gets or sets Data
            /// </summary>
            [DataMember(Name = "Data")]
            public dynamic Data { get; set; }
        }
        #endregion
  
}

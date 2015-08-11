using Nop.Core.Domain.ERPIntegration;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Admin.Models.ERPIntegrationProcess
{
    public partial class ERPIntegrationProcessModel : BaseNopEntityModel
    {
        /// <summary>
        /// Gets or sets the order id
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the ERP integration steps id
        /// </summary>
        public int ERPIntegrationStepsId { get; set; }

        /// <summary>
        /// Gets or sets the ERP integration status id
        /// </summary>
        public int ERPIntegrationStatusId { get; set; }

        /// <summary>
        /// Gets or sets the queue last request date time
        /// </summary>
        public DateTime QueueLastRequestDateTime { get; set; }

        /// <summary>
        /// Gets or sets the queue last request data
        /// </summary>
        public string QueueLastRequestData { get; set; }

        /// <summary>
        /// Gets or sets the queue last response date time
        /// </summary>
        public DateTime QueueLastResponseDateTime { get; set; }

        /// <summary>
        /// Gets or sets the queue last response data
        /// </summary>
        public string QueueLastResponseData { get; set; }

        /// <summary>
        /// Gets or sets the queue status
        /// </summary>
        public string QueueStatus { get; set; }

        /// <summary>
        /// Gets or sets the GetStatus last request date time
        /// </summary>
        public DateTime GetStatusLastRequestDateTime { get; set; }

        /// <summary>
        /// Gets or sets the GetStatus last request data
        /// </summary>
        public string GetStatusLastRequestData { get; set; }

        /// <summary>
        /// Gets or sets the GetStatus last response date time
        /// </summary>
        public DateTime GetStatusLastResponseDateTime { get; set; }

        /// <summary>
        /// Gets or sets the GetStatus last response data
        /// </summary>
        public string GetStatusLastResponseData { get; set; }

        /// <summary>
        /// Gets or sets the GetResponse status
        /// </summary>
        public string GetStatusStatus { get; set; }

        /// <summary>
        /// Gets or sets the GetResponse last request date time
        /// </summary>
        public DateTime GetResponseLastRequestDateTime { get; set; }

        /// <summary>
        /// Gets or sets the GetResponse last request data
        /// </summary>
        public string GetResponseLastRequestData { get; set; }

        /// <summary>
        /// Gets or sets the GetResponse last response date time
        /// </summary>
        public DateTime GetResponseLastResponseDateTime { get; set; }

        /// <summary>
        /// Gets or sets the GetResponse last response data
        /// </summary>
        public string GetResponseLastResponseData { get; set; }

        /// <summary>
        /// Gets or sets the GetResponse status
        /// </summary>
        public string GetResponseStatus { get; set; }

        /// <summary>
        /// Gets or sets the retry count
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// Gets or sets the ERP integration steps
        /// </summary>
        public string ERPIntegrationSteps { get; set; }

        /// <summary>
        /// Gets or sets the ERP integration status
        /// </summary>
        public string ERPIntegrationStatus { get; set; }

        /// <summary>
        /// Gets or sets the Serial no
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// Gets or sets the current latest data
        /// </summary>
        public string LatestData { get; set; }

        /// <summary>
        /// Gets or sets the latest date time
        /// </summary>
        public DateTime LatestDateTime { get; set; }
    }
}

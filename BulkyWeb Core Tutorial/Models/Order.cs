using MGTConcerts.Data;
using MGTConcerts.Repository;
using MGTConcerts.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using QRCoder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using static MGTConcerts.Utilities.Printer;

namespace MGTConcerts.Models
{
    public class Order : IPrintingObject
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? SurName { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string? Email { get; set; }

        public int ConcertId { get; set; }
        [ForeignKey("ConcertId")]
        [ValidateNever]
        public Concert? Concert { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        public Boolean Present { get; set; }
        public DataSet GetPrintingDataSet(string[] fields, IUnitOfWork uow)
        {
            DataSet set = new DataSet("PrintingObjects");
            List<string> sList = fields.ToList();

            Concert con = uow.Concert.Get(u=>u.Id==ConcertId);           

            //con.QRCode= QrBitmap;
            // Fetch Order data
            DataTable orderTable = new ListtoDataTableConverter().ToDataTable(new List<Order>(new[] { this }), "PrintingObject", fields);
            
            // Fetch Concert data
            List<string> concertFields = new List<string> { "Id", "ConcertName", "ConcertDate", "Price"};
            DataTable concertTable = new ListtoDataTableConverter().ToDataTable(new List<Concert> { con }, "Concert", concertFields.ToArray());

            // Add Order and Concert tables to the dataset
            set.Tables.Add(orderTable);
            set.Tables.Add(concertTable);

            return set;
        }

       
    }    
         
}

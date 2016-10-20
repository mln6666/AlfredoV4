using MinimercadoAlfredo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinimercadoAlfredo.ViewModels
{
    public class PurchaseVM
    {
        public Provider Provider { get; set; }

        public DateTime PurchaseDate { get; set; }

        public List<SaleLine> PurchaseLines { get; set; }

        public float PurchaseTotal { get; set; }

        public string Comments { get; set; }
    }
}
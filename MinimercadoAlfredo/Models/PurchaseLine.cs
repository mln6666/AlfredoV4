using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MinimercadoAlfredo.Models
{
    public class PurchaseLine
    {
        [Key]
        public int IdPurchaseLine { get; set; }

        public decimal PurchaseQuantity { get; set; }

        public decimal Cost { get; set; }

        public decimal LineTotal { get; set; }

        public int IdPurchase { get; set; } //Clave Foránea de Purchase

        public virtual Purchase Purchase { get; set; }

        public int IdProduct { get; set; } //Clave Foránea de Producto

        public virtual Product Product { get; set; }
    }
}
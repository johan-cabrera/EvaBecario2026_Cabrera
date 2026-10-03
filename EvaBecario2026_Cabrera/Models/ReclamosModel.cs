using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EvaBecario2026_Cabrera.Models
{
    public class ReclamosModel
    {
        // Reclamos ID
        [Display(Name = "N° Reclamo")]
        public int IdReclamo { get; set; }

        // Datos del proveedor
        [Required(ErrorMessage = "El nombre del proveedor es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del proveedor no puede exceder 50 caracteres.")]
        [Display(Name = "Proveedor")]
        public string NombreProveedor { get; set; }

        [Required(ErrorMessage = "La dirección del proveedor es obligatoria.")]
        [StringLength(100, ErrorMessage = "La dirección no puede exceder 100 caracteres.")]
        [Display(Name = "Dirección Proveedor")]
        public string DireccionProveedor { get; set; }

        // Datos del consumidor
        [Required(ErrorMessage = "Los nombres del consumidor son obligatorios.")]
        [StringLength(50, ErrorMessage = "Los nombres no pueden exceder 50 caracteres.")]
        [Display(Name = "Nombres")]
        public string NombresConsumidor { get; set; }

        [Required(ErrorMessage = "Los apellidos del consumidor son obligatorios.")]
        [StringLength(50, ErrorMessage = "Los apellidos no pueden exceder 50 caracteres.")]
        [Display(Name = "Apellidos")]
        public string ApellidosConsumidor { get; set; }

        // DUI
        [Required(ErrorMessage = "El DUI es obligatorio.")]
        [RegularExpression(@"^\d{8}-\d{1}$", ErrorMessage = "El formato de DUI debe ser 00000000-0.")]
        [Display(Name = "DUI")]
        public string DUI { get; set; }

        // Datos del reclamo
        [Required(ErrorMessage = "El detalle del reclamo es obligatorio.")]
        [StringLength(250, ErrorMessage = "El detalle no puede exceder 250 caracteres.")]
        [Display(Name = "Detalle")]
        public string DetalleReclamo { get; set; }

        [Required(ErrorMessage = "El monto reclamado es obligatorio.")]
        [Range(0.01, 9999999999999999.99, ErrorMessage = "El monto no puede ser negativo ni menor a 0.01.")]
        [Display(Name = "Monto reclamado")]
        public decimal? MontoReclamado { get; set; }

        // Datos de Contacto
        [StringLength(10, ErrorMessage = "El teléfono no puede exceder 10 caracteres.")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        // Datos de registro
        [Display(Name = "Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }

        // Propiedades
        [Display(Name = "Consumidor")]
        public string NombreCompletoConsumidor => $"{NombresConsumidor} {ApellidosConsumidor}";
    }
}

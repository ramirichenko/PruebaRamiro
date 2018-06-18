using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Web;

namespace Api.Models
{
    [Table("Inventory")]
    public class InventoryItem
    {

        #region Fields

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }


        [Column(Order = 1)]
        [MaxLength(255)]
        public string name { get; set; }

        [Column(Order = 2)]
        [MaxLength(255)]
        public string type { get; set; } //Este campo podría ser una cláve foránea, que referenciase a una tabla de tipos existentes, para asi conservar mejor la estructura de los datos, pero por temas de tiempo y de ser mas parte de diseño de la base de datos, lo dejare como un string para que guarde y enseñe la información y ya está.

        [Column(TypeName = "datetime")]
        public DateTime expirationDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime registrationDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? outputDate { get; set; }

        public string registerUser { get; set; }
        public string outputUser { get; set; }


        #endregion Fields

        #region Constructors

      
        public InventoryItem()
        {
           
        }


        #endregion Constructors


        #region Methods

        //Metodo que valida la informacion recibida de un item antes de guardar, devolvemos un codigo int que se correspondera con el codigo http devuelto en la api.
        public int Validate()
        {
            if (string.IsNullOrWhiteSpace(name) || expirationDate == null) return 400;

            return 200;
        }

        public int InputValidate()
        {
            if (string.IsNullOrWhiteSpace(name) || expirationDate == null) return 400; //peticion incorrecta, faltan datos.

            return 200;
        }

        public int OutputValidate()
        {
            if (string.IsNullOrWhiteSpace(name)) return 404; //el elemento no existe

            if (outputDate.HasValue) return 400; //El elemento ya no esta disponible en el inventario

            return 200;
        }



        /// <summary>
        /// Metodo para insertar un elemento en DB
        /// </summary>
        public void InsertItem()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            using (DataContext context = new DataContext())
            {
                try
                {
                    registrationDate = DateTime.UtcNow;
                    registerUser = identity.Name;
                    context.Inventory.Add(this);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Metodo para sacar un elemento del inventario
        /// </summary>
        /// <param name="id"></param>
        public void OutputItem(int id)
        {

            var identity = Thread.CurrentPrincipal.Identity;
            using (DataContext context = new DataContext())
            {
                try
                {
                    InventoryItem invItem = context.Inventory.FirstOrDefault(i => i.id.Equals(id));
                    invItem.outputUser = identity.Name;
                    invItem.outputDate = DateTime.UtcNow;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Metodo para recuperar un elemento por nombre
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public InventoryItem GetItem(string name)
        {
            InventoryItem invItem = new InventoryItem();
            using (DataContext context = new DataContext())
            {
                try
                {
                    invItem = context.Inventory.FirstOrDefault(i => i.name.Equals(name));
                    return invItem;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Metodo para obtener todos los elementos que concidan con el nombre recibido
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<InventoryItem> GetItems(string name)
        {
            List<InventoryItem> invItem = new List<InventoryItem>();
            using (DataContext context = new DataContext())
            {
                try
                {
                    invItem = context.Inventory.Where(i => i.name.Equals(name)).ToList();
                    return invItem;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Obtener todos los elementos de la tabla de inventario
        /// </summary>
        /// <returns></returns>
        public List<InventoryItem> GetItemList()
        {
            List<InventoryItem> invItems = new List<InventoryItem>();
            using (DataContext context = new DataContext())
            {
                try
                {
                    invItems = context.Inventory.ToList();
                    return invItems.Count == 0 ? null : invItems;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Obtener todos los elementos que expiran hoy
        /// </summary>
        /// <returns></returns>
        public List<InventoryItem> GetExpired()
        {
            List<InventoryItem> invItems = new List<InventoryItem>();
            DateTime now = DateTime.UtcNow.Date;
            using (DataContext context = new DataContext())
            {
                try
                {
                    invItems = context.Inventory.Where(i => i.expirationDate.Equals(now)).ToList();
                    return invItems.Count == 0 ? null : invItems;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }   

        #endregion Methods

    }
}
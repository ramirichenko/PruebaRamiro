using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Front.Model.Models
{

    //Cuando añado un elemento al inventario, éste contiene información sobre el elemento que se acaba de añadir, tal como Nombre,
    //Fecha de caducidad, Tipo, etc
    public class InventoryItem
    {
        public int id { get; set; }
        public string name { get; set; }     
        public string type { get; set; } //Este campo podría ser una cláve foránea, que referenciase a una tabla de tipos existentes, para asi conservar mejor la estructura de los datos, pero por temas de tiempo y de ser mas parte de diseño de la base de datos, lo dejare como un string para que guarde y enseñe la información y ya está.
        public DateTime expirationDate { get; set; }
        public DateTime registrationDate { get; set; }
        public DateTime? outputDate { get; set; }


        public InventoryItem()
        {

        }

        public InventoryItem(string name, string type, DateTime expirationDate)
        {
            this.name = name;
            this.type = type;
            this.expirationDate = expirationDate;
        }
    }
}
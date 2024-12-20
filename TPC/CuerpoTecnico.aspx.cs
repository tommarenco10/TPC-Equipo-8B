﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC
{
    public partial class CuerpoTecnico : System.Web.UI.Page
    {
        public List<Entrenador> Entrenadores { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            CuerpoTecnicoNegocio negocio = new CuerpoTecnicoNegocio();
            Entrenadores = negocio.Listar();
        }
    }
}
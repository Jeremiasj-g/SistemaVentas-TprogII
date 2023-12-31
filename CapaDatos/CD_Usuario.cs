﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidad;  // Importa la clase de entidad definida en otro archivo.

namespace CapaDatos
{
    public class CD_Usuario
    {
        // Método que recupera una lista de objetos de tipo 'Usuario' desde la base de datos.
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();  // Inicializa una lista vacía de objetos 'Usuario'.

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = "select IdUsuario, Documento, NombreCompleto, Correo, Clave, Estado from usuario";

                    // Crea un comando SQL para ejecutar la consulta.
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();  // Abre la conexión a la base de datos.

                    using (SqlDataReader dr = cmd.ExecuteReader())  // Ejecuta la consulta y obtiene un lector de datos.
                    {
                        while (dr.Read()) {
                            lista.Add(new Usuario()
                            {
                                // Crea un objeto 'Usuario' y asigna valores desde la base de datos.
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                } catch (Exception ex)
                {
                    lista = new List<Usuario>();  // En caso de error, reinicia la lista como vacía.
                }
            }

            return lista;  // Devuelve la lista de usuarios recuperada de la base de datos.
        }
    }
}

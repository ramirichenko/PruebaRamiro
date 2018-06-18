using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Api.Utils
{
    //Clase para gestionar y guardar logs de la aplicación, en este caso solo he puesto logs en la consola de la aplicacion, pero podría implementarse logs en ficheros
    //en la consola de Google Cloud, AWS , registro de eventos de windows o el que sea.
    public static class Logger
    {

        public static void Info(string error, string endpoint)
        {
            Console.WriteLine(string.Format("INFO  || Date: {0} || Error {1} || Endpoint {2}", DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ss"),error,endpoint));
            Debug.WriteLine(string.Format("INFO  || Date: {0} || Error {1} || Endpoint {2}", DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ss"),error,endpoint));
        }

        public static void Warn(string error, string endpoint)
        {
            Console.WriteLine(string.Format("WARN  || Date: {0} || Error {1} || Endpoint {2}", DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ss"), error, endpoint));
            Debug.WriteLine(string.Format("WARN  || Date: {0} || Error {1} || Endpoint {2}", DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ss"), error, endpoint));
        }

        public static void Error(string error, string endpoint)
        {
            Console.WriteLine(string.Format("ERROR  || Date: {0} || Error {1} || Endpoint {2}", DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ss"), error, endpoint));
            Debug.WriteLine(string.Format("ERROR  || Date: {0} || Error {1} || Endpoint {2}", DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ss"), error, endpoint));
        }

    }
}
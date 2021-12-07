using Alura.ListaLeitura.Modelos;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Formatters
{
    public class LivroCsvFormatter : TextOutputFormatter
    {
        public LivroCsvFormatter()
        {
            var textCsvMediaType = MediaTypeHeaderValue.Parse("text/csv");          //Passando o valor desse MediaType
            var appCsvMediaType = MediaTypeHeaderValue.Parse("application/csv");    //Passando o valor desse MediaType
            SupportedMediaTypes.Add(textCsvMediaType);  //Adicionando o MediaType
            SupportedMediaTypes.Add(appCsvMediaType);   //Adicionando o MediaType
            SupportedEncodings.Add(Encoding.UTF8);
        }

        protected override bool CanWriteType(Type type)     //O método WriteResponse só será chamado se a condição abaixo for aceita
        {
            return type == typeof(LivroApi);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var livroEmCsv = "";

            if (context.Object is LivroApi) //se o objeto dentro do contexto for do tipo LivroApi
            {
                var livro = context.Object as LivroApi; //pegamos o objeto

                livroEmCsv = $"{livro.Titulo};{livro.Subtitulo};{livro.Autor};{livro.Lista}"; //e passamos as informações dele para formato csv
            }
            

            using (var escritor = context.WriterFactory(context.HttpContext.Response.Body, selectedEncoding)) //context.HttpContext.Response.Body   //Escreve no corpo da Response (Stream)
            {
                return escritor.WriteAsync(livroEmCsv);
            }//escritor.Close();
        }
    }
}

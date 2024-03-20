using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Common
{
    public class DataDictionary
    {
        public const string CHAVE_ENCRIPTACAO = "fTyKPtEzhYsSKQh1wrKOUPNFzevB8P";
        public const int CODIGO_PIX = 2;

        //Tipos de Usuarios
        public const int USUARIO_ADMINISTRADOR = 1;
        public const int USUARIO_CLIENTE = 2;

        //Status de Pedido
        public const int STATUS_PEDIDO_PENDENTE = 1;
        public const int STATUS_PEDIDO_PAGO = 2;
        public const int STATUS_PEDIDO_CANCELADO = 4;


        //Dias maximo pagamento
        public const int DIAS_MAXIMO_PAGAMENTO = 2;
    }
}

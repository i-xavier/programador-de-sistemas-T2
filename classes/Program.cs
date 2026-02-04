using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classes
{
    // Definição da classe carro
    internal class Carro
    {
        // Propriedade pública: Pode ser acessada e modificada fora da classe
        public string Marca { get; set; } // Publica, propriedade Marca
        
        // Propriedade privada: Não pode ser acessada ou modificada fora da classe
        private string Modelo { get; set; } //Privada, acessível apenas dentro da classe

        // Propriedade privada com um campo privado e um método getter e setter personalizados
        private int ano; // Campo privado

        public int Ano
        {
            get { return ano; } // Getter, para acessar o valor do campo privado

            set
            {
                if (value > 1900 && value <= DateTime.Now.Year) //Verificação para garantir um valor válido
                {
                    ano = value;
                }
                else 
                {
                    Console.WriteLine("Ano inválido.");
                }
            }
        }
        

        // Método público para exibir as informações do carro

        public void ExibirInformacoes()
        {
            Console.WriteLine($"Marca: {Marca}");
            Console.WriteLine($"Modelo: {Modelo}"); // Acesso ao modelo é restrito por ser privado
            Console.WriteLine($"Ano: {Ano}");
        }
        

        // Método para calcular a idade do carro
        public int CalcularIdade()
        {
            int anoAtual = DateTime.Now.Year;
            return anoAtual - Ano;

        }

        // Método para definir o modelo do carro (com exemplo de uso de properiedade privada)
        public void DefinirModelo(string modelo)
        {
            this.Modelo = modelo; // Acessando a propriedade privada dentro da classe
        }
    
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace exLinqFila
{
    internal class Aluno
    {
        public int Prontuario { get; set; }
        public string Nome { get; set; }
        public double Nota { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<Aluno> filaAlunos;
            filaAlunos = new Queue<Aluno>();

            // Enqueue: insere no final da fila
            filaAlunos.Enqueue(new Aluno { Prontuario = 1, Nome = "Ana", Nota = 8.5 });
            filaAlunos.Enqueue(new Aluno { Prontuario = 2, Nome = "Bruno", Nota = 5.0 });
            filaAlunos.Enqueue(new Aluno { Prontuario = 3, Nome = "Carla", Nota = 9.2 });
            filaAlunos.Enqueue(new Aluno { Prontuario = 4, Nome = "Daniel", Nota = 6.8 });
            filaAlunos.Enqueue(new Aluno { Prontuario = 5, Nome = "Alice", Nota = 7.4 });
            mostraFila("Fila original (frente -> final)", filaAlunos);

            // Peek() é a operação legítima de fila: consulta a frente, O(1), sem remover.
            Console.WriteLine("Frente da fila (Peek): {0}", filaAlunos.Peek().Nome);

            // First() e Last() NÃO são operações de fila: vêm do LINQ.
            // Queue<T> enumera da frente para o final, então First() = Peek().
            Console.WriteLine("First() (LINQ, = frente): {0}", filaAlunos.First().Nome);
            Console.WriteLine("Last()  (LINQ, = final, percorre tudo - O(n)): {0}", filaAlunos.Last().Nome);
            Console.WriteLine();

            // 1) Where + OrderBy (sintaxe de métodos): aprovados (nota >= 6), por nome
            var aprovados = filaAlunos
                .Where(a => a.Nota >= 6.0)
                .OrderBy(a => a.Nome);
            mostraFila("Aprovados (métodos)", aprovados);

            // 2) Mesma consulta em sintaxe de consulta (semelhante ao SQL)
            //    var é inferência de tipo: o tipo real aqui é IOrderedEnumerable<Aluno>
            var aprovados2 = from a in filaAlunos
                             where a.Nota >= 6.0
                             orderby a.Nome
                             select a;
            mostraFila("Aprovados (consulta)", aprovados2);

            // 2b) Declaração com tipo explícito, equivalente à anterior
            IEnumerable<Aluno> aprovados3 = from a in filaAlunos
                                            where a.Nota >= 6.0
                                            orderby a.Nome
                                            select a;
            mostraFila("Aprovados (tipo explícito)", aprovados3);

            // 3) Busca por nome iniciado em "A" (mantém a ordem da fila)
            var comA = filaAlunos.Where(a => a.Nome.StartsWith("A"));
            mostraFila("Nomes iniciados em A", comA);

            // 4) Busca de um único elemento: FirstOrDefault retorna null se não achar
            Aluno achado = filaAlunos.FirstOrDefault(a => a.Prontuario == 3);
            Console.WriteLine("Prontuário 3: {0}", achado != null ? achado.Nome : "não encontrado");

            // 5) Agregações e verificações
            Console.WriteLine("Média das notas: {0:F2}", filaAlunos.Average(a => a.Nota));
            Console.WriteLine("Maior nota: {0}", filaAlunos.Max(a => a.Nota));
            Console.WriteLine("Algum reprovado? {0}", filaAlunos.Any(a => a.Nota < 6.0));
            Console.WriteLine("Qtde de aprovados: {0}", filaAlunos.Count(a => a.Nota >= 6.0));

            // 6) Projeção: apenas os nomes, em uma lista nova
            List<string> nomes = filaAlunos.Select(a => a.Nome).ToList();
            Console.WriteLine("Nomes: {0}", string.Join(", ", nomes));

            // 7) Tipo anônimo: único caso em que var é obrigatório
            var resumo = filaAlunos.Select(a => new { a.Nome, a.Nota });
            foreach (var item in resumo)
            {
                Console.WriteLine("{0}: {1:F1}", item.Nome, item.Nota);
            }

            // 8) LINQ apenas LÊ a fila: nenhuma consulta acima removeu elementos
            Console.WriteLine();
            Console.WriteLine("Qtde na fila após as consultas: {0}", filaAlunos.Count);

            // 9) Dequeue: remove da frente (FIFO). Esta, sim, altera a fila.
            Aluno atendido = filaAlunos.Dequeue();
            Console.WriteLine("Saiu da fila (Dequeue): {0}", atendido.Nome);
            mostraFila("Fila após Dequeue", filaAlunos);

            // 10) Atendimento até esvaziar a fila, combinando Dequeue e LINQ
            while (filaAlunos.Count > 0)
            {
                Aluno proximo = filaAlunos.Dequeue();
                string situacao = proximo.Nota >= 6.0 ? "aprovado" : "reprovado";
                Console.WriteLine("Atendido: {0} - {1}", proximo.Nome, situacao);
            }
            Console.WriteLine("Fila vazia? {0}", !filaAlunos.Any());

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para encerrar...");
            Console.ReadKey();
        }

        static void mostraFila(string titulo, IEnumerable<Aluno> alunos)
        {
            Console.WriteLine("-- {0} ------", titulo);
            foreach (Aluno a in alunos)
            {
                Console.WriteLine("{0} - {1} ({2:F1})", a.Prontuario, a.Nome, a.Nota);
            }
            Console.WriteLine("---------------");
        }
    }
}

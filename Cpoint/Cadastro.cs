using System.Collections.Generic;

namespace Cpoint
{
    public class Cadastro<T>
    {
        private Dictionary<int, T> dados;

        public Cadastro()
        {
            dados = new Dictionary<int, T>();
        }

        public void Adicionar(int id, T item)
        {
            if (dados.ContainsKey(id))
            {
                throw new System.Exception($"ID {id} já existe no cadastro.");
            }
            dados.Add(id, item);
        }

        public List<KeyValuePair<int, T>> Listar()
        {
            return new List<KeyValuePair<int, T>>(dados);
        }

        public T Buscar(int id)
        {
            if (dados.ContainsKey(id))
            {
                return dados[id];
            }
            throw new System.Exception($"ID {id} não encontrado.");
        }

        public void Remover(int id)
        {
            if (!dados.ContainsKey(id))
            {
                throw new System.Exception($"ID {id} não encontrado.");
            }
            dados.Remove(id);
        }

        public int Count()
        {
            return dados.Count;
        }

        public void Limpar()
        {
            dados.Clear();
        }
    }
}

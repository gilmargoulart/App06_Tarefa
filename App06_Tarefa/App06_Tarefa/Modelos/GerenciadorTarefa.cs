using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace App06_Tarefa.Modelos
{
    public class GerenciadorTarefa
    {
        private List<Tarefa> Tarefas { get; set; }
        private const string PropertyKey = "tarefas";

        public GerenciadorTarefa()
        {
            Tarefas = ListagemNoProperties();
        }

        public void Salvar(Tarefa tarefa)
        {
            Tarefas.Add(tarefa);
            SalvarProperty();
        }
        public void Deletar(int index)
        {
            Tarefas.RemoveAt(index);
            SalvarProperty();
        }
        public void Finalizar(int index, Tarefa tarefa)
        {
            Tarefas.RemoveAt(index);
            tarefa.DataFinalizacao = DateTime.Now;
            Tarefas.Add(tarefa);
            SalvarProperty();
        }

        public List<Tarefa> Listagem()
        {
            return Tarefas;
        }

        private List<Tarefa> ListagemNoProperties()
        {
            if (App.Current.Properties.ContainsKey(PropertyKey))
            {
                string json = (String)App.Current.Properties[PropertyKey];
                return JsonConvert.DeserializeObject<List<Tarefa>>(json); ;
            }
            return new List<Tarefa>();
        }

        private void SalvarProperty()
        {
            if (App.Current.Properties.ContainsKey(PropertyKey))
            {
                App.Current.Properties.Remove(PropertyKey);
            }

            string json = JsonConvert.SerializeObject(Tarefas);
            App.Current.Properties.Add(PropertyKey, json);
        }

    }
}

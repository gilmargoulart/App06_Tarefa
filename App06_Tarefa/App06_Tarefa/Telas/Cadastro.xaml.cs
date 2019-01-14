using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App06_Tarefa.Modelos;

namespace App06_Tarefa.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Cadastro : ContentPage
	{
        private byte Prioridade { get; set; } = 0;
		public Cadastro ()
		{
			InitializeComponent ();
		}

        private void TapGestureRecognizer_Prioridade(object sender, EventArgs e)
        {
            var stacks = slPrioridades.Children;
            foreach (var stack in stacks)
            {
                Label lblPrioridades = ((StackLayout)stack).Children[1] as Label;
                lblPrioridades.TextColor = Color.Gray;
            }
            ((Label)((StackLayout)sender).Children[1]).TextColor = Color.Black;
            FileImageSource fis = ((Image)((StackLayout)sender).Children[0]).Source as FileImageSource;
            string prioridade = fis.File.Replace("Resources/", "").Replace(".png", "").Replace("p", "");
            Prioridade = Byte.Parse(prioridade);
        }

        private void Button_Salvar(object sender, EventArgs args)
        {
            bool hasErrors = false;
            try
            {
                if (!(txtNome.Text.Trim().Length > 0))
                {
                    hasErrors = true;
                    DisplayAlert("Erro", "Nome é obrigatório.", "Ok");
                }
            }
            catch (NullReferenceException)
            {
                hasErrors = true;
                DisplayAlert("Erro", "Nome é obrigatório.", "Ok");
            }

            if (Prioridade <= 0)
            {
                hasErrors = true;
                DisplayAlert("Erro", "Selecione a prioridade.", "Ok");
            }
            if (!hasErrors)
            {
                Tarefa tarefa = new Tarefa()
                {
                    Nome = txtNome.Text.Trim(),
                    Prioridade = this.Prioridade
                };
                new GerenciadorTarefa().Salvar(tarefa);
                DisplayAlert("Tarefa Salva", "Salvo com sucesso.", "Concluir");

                App.Current.MainPage = new NavigationPage(new Inicio());
            }
        }
    }
}
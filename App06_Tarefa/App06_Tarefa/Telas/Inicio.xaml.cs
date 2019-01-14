using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App06_Tarefa.Modelos;
using System.Globalization;

namespace App06_Tarefa.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Inicio : ContentPage
	{
		public Inicio ()
		{
			InitializeComponent ();

            CultureInfo culture = CultureInfo.CurrentCulture;
            string txtData = DateTime.Now.ToString("dddd, dd {0} MMMM {0} yyyy", culture);
            txtDataHoje.Text = string.Format(txtData, "de");

            CarregarTarefas();

        }

        private void GoCadastro(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Cadastro());
        }

        public void LinhaStackLayout(Tarefa tarefa, int index)
        {
            View txtTarefa = null;
            if (tarefa.DataFinalizacao == null)
            {
                txtTarefa = new Label()
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Text = tarefa.Nome
                };
            }
            else
            {
                txtTarefa = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Spacing = 0,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                ((StackLayout)txtTarefa).Children.Add(new Label() {Text = tarefa.Nome, TextColor = Color.Gray});
                ((StackLayout)txtTarefa).Children.Add(new Label() {Text = "Finalizado em " + tarefa.DataFinalizacao.Value.ToString("dd/MM/yyyy hh:mm"), TextColor = Color.Gray, FontSize = 10});
            }

            string imgCheck = "CheckOff.png";
            if (tarefa.DataFinalizacao != null)
            {
                imgCheck = "CheckOn.png";
            }
            Image check = new Image()
            {
                VerticalOptions = LayoutOptions.Center,
                Source = ImageSource.FromFile(imgCheck)
            };
            if (Device.RuntimePlatform == Device.UWP)
            {
                check.Source = ImageSource.FromFile("Resources/"+ imgCheck);
            }
            
            TapGestureRecognizer imgCheckTap = new TapGestureRecognizer();
            imgCheckTap.Tapped += delegate
            {
                new GerenciadorTarefa().Finalizar(index, tarefa);
                CarregarTarefas();
            };
            check.GestureRecognizers.Add(imgCheckTap);

            Image imgPrioridade = new Image()
            {
                VerticalOptions = LayoutOptions.Center,
                Source = ImageSource.FromFile("p"+tarefa.Prioridade+".png")
            };
            if (Device.RuntimePlatform == Device.UWP)
            {
                imgPrioridade.Source = ImageSource.FromFile("Resources/p" + tarefa.Prioridade +".png");
            }

            Image imgDelete = new Image()
            {
                VerticalOptions = LayoutOptions.Center,
                Source = ImageSource.FromFile("Delete.png")
            };
            if (Device.RuntimePlatform == Device.UWP)
            {
                imgDelete.Source = ImageSource.FromFile("Resources/Delete.png");

            }
            TapGestureRecognizer imgDeleteTap = new TapGestureRecognizer();
            imgDeleteTap.Tapped += delegate
            {
                new GerenciadorTarefa().Deletar(index);
                CarregarTarefas();
            };
            imgDelete.GestureRecognizers.Add(imgDeleteTap);
            
                
            StackLayout linha = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 15
            };

            linha.Children.Add(check);
            linha.Children.Add(txtTarefa);
            linha.Children.Add(imgPrioridade);
            linha.Children.Add(imgDelete);

            slTarefas.Children.Add(linha);
        }

        private void CarregarTarefas()
        {
            slTarefas.Children.Clear();

            int i = 0;
            List<Tarefa> tarefas = new GerenciadorTarefa().Listagem();
            foreach (Tarefa tarefa in tarefas)
            {
                LinhaStackLayout(tarefa, i);
                i++;
            }
        }
    }
}
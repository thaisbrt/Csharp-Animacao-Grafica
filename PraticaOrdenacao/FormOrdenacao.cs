using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Pratica5 {
    public partial class FormOrdenacao : Form {

        string VerificarOrdenacao;
        int[] vet = new int[500]; // vetor interno para a animação

        private void EscolhaTam_TextChanged(object sender, EventArgs e)
        {
            vet = new int[Convert.ToInt32(((TextBox)sender).Text)];

            Crescente.Enabled = true;
            Aleatorio.Enabled = true;
            Decrescente.Enabled = true;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            algoritmoToolStripMenuItem.Enabled = true;
            estatísticasToolStripMenuItem.Enabled = true;
        }

        private void Crescente_CheckedChanged(object sender, EventArgs e)
        {
            algoritmoToolStripMenuItem.Enabled = true;
            estatísticasToolStripMenuItem.Enabled = true;
        }

        private void Aleatorio_CheckedChanged(object sender, EventArgs e)
        {
            algoritmoToolStripMenuItem.Enabled = true;
            estatísticasToolStripMenuItem.Enabled = true;
        }

        public FormOrdenacao() {
            InitializeComponent();
            panel.Paint += new PaintEventHandler(panel_Paint);
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panel, new object[] { true });
        }

        private void panel_Paint(object sender, PaintEventArgs e) {
            for (int i = 0; i < vet.Length; i++) {
                e.Graphics.DrawLine(Pens.Blue, i, 299, i, 299 - vet[i]);
            }
        }

        private void bolhaToolStripMenuItem_Click(object sender, EventArgs e) {
            iniciaAnimacao(() => OrdenacaoGrafica.Bolha(vet, panel));
        }

        // TODO: animação e estatísticas dos demais métodos de ordenação

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show(this, 
                "Prática 5 2023/1 - Métodos de Ordenação\n\n" +
                "Desenvolvido por:\n72201266 - Thais Brasileiro Torres\n" +
                "Prof. Virgílio Borges de Oliveira\n\n" +
                "Algoritmos e Estruturas de Dados\n" +
                "Faculdade COTEMIG\n" +
                "Apenas para fins didáticos.", 
                "Sobre o trabalho...", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
        }

        private void verificarpreenchimento()
        {
            if (Crescente.Checked)
            {
                Preenchimento.Crescente(vet, 300);
                VerificarOrdenacao = "Crescente";
;            }
            else if (Decrescente.Checked)
            {
                Preenchimento.Decrescente(vet, 300);
                VerificarOrdenacao = "Decrescente";
            }
            else
            {
                Preenchimento.Aleatorio(vet, 300);
                VerificarOrdenacao = "Aleatorio";
            }
        }

        private void iniciaAnimacao(Action a) {
            if (bgw.IsBusy != true) {

                verificarpreenchimento();
                bgw.RunWorkerAsync(a);
            }
            else {
                MessageBox.Show(this,
                   "Aguarde o fim da execução atual...",
                   "Prática 5 2023/1 - Métodos de Ordenação",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation);
            }
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e) {
            Action a = (Action)e.Argument;
            a();
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            MessageBox.Show(this,
               "Animação concluída!",
               "Prática 5 2023/1 - Métodos de Ordenação",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information);
        }

        private void bolhaToolStripMenuItem1_Click_1(object sender, EventArgs e) {
            verificarpreenchimento();
            var stopwatch = new Stopwatch();
            stopwatch.Start(); // inicia cronômetro
            OrdenacaoEstatistica.Bolha(vet);

            stopwatch.Stop(); // interrompe cronômetro
            long elapsed_time = stopwatch.ElapsedMilliseconds; // calcula o tempo decorrido

            MessageBox.Show(this,
                  $"Tamanho do vetor: {vet.Length} \nOrdenação inicial: {VerificarOrdenacao} \nTempo de execução: " 
                  + String.Format("{0:F4} seg", elapsed_time / 1000.0) +
                  $"\nNº de trocas: {OrdenacaoEstatistica.cont_t} \nNº de comparações: {OrdenacaoEstatistica.cont_c}",
                  "Estatísticas do Método Bolha",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
        }

        private void seleçãoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            verificarpreenchimento();
            var stopwatch = new Stopwatch();
            stopwatch.Start(); // inicia cronômetro
            OrdenacaoEstatistica.Selecao(vet);
            stopwatch.Stop(); // interrompe cronômetro
            long elapsed_time = stopwatch.ElapsedMilliseconds; // calcula o tempo decorrido

            MessageBox.Show(this,
                  $"Tamanho do vetor: {vet.Length} \nOrdenação inicial: {VerificarOrdenacao} \nTempo de execução: "
                  + String.Format("{0:F4} seg", elapsed_time / 1000.0) +
                  $"\nNº de trocas: {OrdenacaoEstatistica.cont_t} \nNº de comparações: {OrdenacaoEstatistica.cont_c}",
                  "Estatísticas do Método Seleção",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
        }

        private void inserçãoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            verificarpreenchimento();
            var stopwatch = new Stopwatch();
            stopwatch.Start(); // inicia cronômetro
            OrdenacaoEstatistica.Insercao(vet);
            stopwatch.Stop(); // interrompe cronômetro
            long elapsed_time = stopwatch.ElapsedMilliseconds; // calcula o tempo decorrido

            MessageBox.Show(this,
                  $"Tamanho do vetor: {vet.Length} \nOrdenação inicial: {VerificarOrdenacao} \nTempo de execução: "
                  + String.Format("{0:F4} seg", elapsed_time / 1000.0) +
                  $"\nNº de trocas: {OrdenacaoEstatistica.cont_t} \nNº de comparações: {OrdenacaoEstatistica.cont_c}",
                  "Estatísticas do Método Inserção",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
        }

        private void shellSortToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            verificarpreenchimento();
            var stopwatch = new Stopwatch();
            stopwatch.Start(); // inicia cronômetro
            OrdenacaoEstatistica.ShellSort(vet);
            stopwatch.Stop(); // interrompe cronômetro
            long elapsed_time = stopwatch.ElapsedMilliseconds; // calcula o tempo decorrido

            MessageBox.Show(this,
                  $"Tamanho do vetor: {vet.Length} \nOrdenação inicial: {VerificarOrdenacao} \nTempo de execução: "
                  + String.Format("{0:F4} seg", elapsed_time / 1000.0) +
                  $"\nNº de trocas: {OrdenacaoEstatistica.cont_t} \nNº de comparações: {OrdenacaoEstatistica.cont_c}",
                  "Estatísticas do Método Shell Sort",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
        }

        private void heapSortToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            verificarpreenchimento();
            var stopwatch = new Stopwatch();
            stopwatch.Start(); // inicia cronômetro
            OrdenacaoEstatistica.HeapSort(vet);
            stopwatch.Stop(); // interrompe cronômetro
            long elapsed_time = stopwatch.ElapsedMilliseconds; // calcula o tempo decorrido

            MessageBox.Show(this,
                  $"Tamanho do vetor: {vet.Length} \nOrdenação inicial: {VerificarOrdenacao} \nTempo de execução: "
                  + String.Format("{0:F4} seg", elapsed_time / 1000.0) +
                  $"\nNº de trocas: {OrdenacaoEstatistica.cont_t} \nNº de comparações: {OrdenacaoEstatistica.cont_c}",
                  "Estatísticas do Método HeapSort",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
        }

        private void quickSortToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            verificarpreenchimento();
            var stopwatch = new Stopwatch();
            stopwatch.Start(); // inicia cronômetro
            OrdenacaoEstatistica.cont_c = 0;
            OrdenacaoEstatistica.cont_t = 0;
            OrdenacaoEstatistica.QuickSort(vet, 0, vet.Length - 1);
            stopwatch.Stop(); // interrompe cronômetro
            long elapsed_time = stopwatch.ElapsedMilliseconds; // calcula o tempo decorrido

            MessageBox.Show(this,
                  $"Tamanho do vetor: {vet.Length} \nOrdenação inicial: {VerificarOrdenacao} \nTempo de execução: "
                  + String.Format("{0:F4} seg", elapsed_time / 1000.0) +
                  $"\nNº de trocas: {OrdenacaoEstatistica.cont_t} \nNº de comparações: {OrdenacaoEstatistica.cont_c}",
                  "Estatísticas do Método Quick Sort",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
        }

        private void mergeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            verificarpreenchimento();
            var stopwatch = new Stopwatch();
            stopwatch.Start(); // inicia cronômetro
            OrdenacaoEstatistica.cont_c = 0;
            OrdenacaoEstatistica.cont_t = 0;
            OrdenacaoEstatistica.MergeSort(vet, 0, vet.Length - 1);
            stopwatch.Stop(); // interrompe cronômetro
            long elapsed_time = stopwatch.ElapsedMilliseconds; // calcula o tempo decorrido

            MessageBox.Show(this,
                  $"Tamanho do vetor: {vet.Length} \nOrdenação inicial: {VerificarOrdenacao} \nTempo de execução: "
                  + String.Format("{0:F4} seg", elapsed_time / 1000.0) +
                  $"\nNº de trocas: {OrdenacaoEstatistica.cont_t} \nNº de comparações: {OrdenacaoEstatistica.cont_c}",
                  "Estatísticas do Método Merge Sort",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
        }

        private void seleçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iniciaAnimacao(() => OrdenacaoGrafica.selecao(vet, panel));
        }

        private void inserçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iniciaAnimacao(() => OrdenacaoGrafica.insercao(vet, panel));
        }

        private void shellsortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iniciaAnimacao(() => OrdenacaoGrafica.shellSort(vet, panel));
        }

        private void heapsortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iniciaAnimacao(() => OrdenacaoGrafica.heapSort(vet, panel));
        }

        private void quicksortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iniciaAnimacao(() => OrdenacaoGrafica.quickSort(vet, 0, vet.Length - 1, panel));
        }

        private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iniciaAnimacao(() => OrdenacaoGrafica.mergeSort(vet, 0, vet.Length - 1, panel));
        }

        // vazio

        private void FormOrdenacao_Load(object sender, EventArgs e)
        {

        }

        private void algoritmoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void estatísticasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}

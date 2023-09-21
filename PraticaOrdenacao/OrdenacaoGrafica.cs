using System.Threading;
using System.Windows.Forms;

namespace Pratica5 {
    class OrdenacaoGrafica {
        // TODO: declarar demais métodos de ordenação
        public static void Bolha(int[] vet, Panel p) {
            int i, j, temp;
            for (i = 0; i < vet.Length - 1; i++) {
                for (j = vet.Length - 1; j > i; j--) {
                    if (vet[j] < vet[j - 1]) {
                        temp = vet[j];
                        vet[j] = vet[j - 1];
                        vet[j - 1] = temp;
                    }
                }
                p.Invalidate(); // redesenha o painel
                Thread.Sleep(10); // espera 10 milisegundos
            }
        }

        public static void selecao(int[] vet, Panel p)
        {
            int i, j, min, temp;
            for (i = 0; i < vet.Length - 1; i++)
            {
                min = i;
                for (j = i + 1; j < vet.Length; j++)
                {
                    if (vet[j] < vet[min])
                    {
                        min = j;
                    }
                }
                temp = vet[i];
                vet[i] = vet[min];
                vet[min] = temp;
                p.Invalidate(); // redesenha o painel
                Thread.Sleep(10); // espera 10 milisegundos
            }
        }

        public static void insercao(int[] vet, Panel p)
        {
            int temp, i, j;
            for (i = 1; i < vet.Length; i++)
            {
                p.Invalidate(); // redesenha o painel
                Thread.Sleep(10); // espera 10 milisegundos

                temp = vet[i];
                j = i - 1;
                while (j >= 0 && temp < vet[j])
                {
                    vet[j + 1] = vet[j];
                    j--;

                    vet[j + 1] = temp;
                }

            }
        }

        public static void shellSort(int[] vet, Panel p)
        {
            int i, j, x, n;
            int h = 1;
            n = vet.Length;
            do
            {
                h = h * 3 + 1;
            } while (h <= n);
            do
            {
                h /= 3;
                for (i = h; i < n; i++)
                {
                    x = vet[i];
                    j = i;
                    while (j > (h - 1) && vet[j - h] > x)
                    {
                        vet[j] = vet[j - h];
                        j -= h;
                    }
                    vet[j] = x;
                    p.Invalidate(); // redesenha o painel
                    Thread.Sleep(5); // espera 10 milisegundos
                }
            } while (h != 1);
        }

        public static void quickSort(int[] vet, int esq, int dir, Panel p)
        {
            int i, j, x, temp;

            x = vet[(esq + dir) / 2]; // pivo
            i = esq;
            j = dir;
            do
            {
                while (x > vet[i]) i++;
                while (x < vet[j]) j--;
                if (i <= j)
                {
                    temp = vet[i];
                    vet[i] = vet[j];
                    vet[j] = temp;
                    i++;
                    j--;
                }

                p.Invalidate(); // redesenha o painel
                Thread.Sleep(3); // espera 10 milisegundos

            } while (i <= j);
            if (esq < j) quickSort(vet, esq, j, p);
            if (i < dir) quickSort(vet, i, dir, p);
        }

        public static void heapSort(int[] vet, Panel p)
        {
            constroiMaxHeap(vet);
            int n = vet.Length;

            for (int i = vet.Length - 1; i > 0; i--)
            {
                troca(vet, i, 0);
                refaz(vet, 0, --n);

                p.Invalidate(); // redesenha o painel
                Thread.Sleep(10); // espera 10 milisegundos
            }
        }

        private static void constroiMaxHeap(int[] vet)
        {
            for (int i = vet.Length / 2 - 1; i >= 0; i--)
                refaz(vet, i, vet.Length);

        }

        private static void refaz(int[] vet, int pos, int tamanhoDoVetor)
        {

            int max = 2 * pos + 1, right = max + 1;
            if (max < tamanhoDoVetor)
            {

                if (right < tamanhoDoVetor && vet[max] < vet[right])
                    max = right;

                if (vet[max] > vet[pos])
                {
                    troca(vet, max, pos);
                    refaz(vet, max, tamanhoDoVetor);
                }
            }
        }

        public static void troca(int[] vet, int j, int aposJ)
        {
            int aux = vet[j];
            vet[j] = vet[aposJ];
            vet[aposJ] = aux;
        }

        private static void merge(int[] v, int i, int m, int j, Panel p)
        {
            int[] temp = new int[m - i + 1];
            int k;
            for (k = i; k <= m; k++)
                temp[k - i] = v[k];
            int esq = 0, dir = m + 1;
            k = m - i + 1;
            while (esq < k && dir <= j)
            {
                if (temp[esq] <= v[dir])
                    v[i++] = temp[esq++];
                else
                    v[i++] = v[dir++];


            }
            while (esq < k)
            {
                v[i++] = temp[esq++];
            }

            p.Invalidate(); // redesenha o painel
            Thread.Sleep(5); // espera 10 milisegundos
        }
        public static void mergeSort(int[] v, int i, int j, Panel p)
        {
            if (i < j)
            {
                int m = (i + j) / 2;
                mergeSort(v, i, m, p);
                mergeSort(v, m + 1, j, p);
                merge(v, i, m, j, p); // intercala v[i..m] e v[m+1..j] em v[i..j] 
            }
        }


    }
}

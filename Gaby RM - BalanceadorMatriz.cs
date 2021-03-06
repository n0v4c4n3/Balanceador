//Gaby RM - BalanceadorMatriz (14/09/2015)
using System;
using System.Collections;
namespace BalanceadorMatriz
{
	public class Utilidades{
		//Numero de vertices "personas"
		private int N { get; set; }
		//Dada una matriz con +-deudas, simplificar las deudas recíprocas
		public void balancear(decimal[][] matriz){
			decimal[N] cantidades = {0};
			for (int p=0; p<N; p++){
				for (int i=0; i<N; i++){
					//Lo que "p" acreditó a "i" menos lo que "i" le debe a "p"
					cantidades[p] += (matriz[p][i] - matriz[i][p]);
				}
			}
			balancearRec(cantidades);
		}		
		public void balancearRec(decimal[] cantidades){	
			decimal mayorDebito = maximo(cantidades);
			decimal mayorCredito = minimo(cantidades); 
			if (cantidades[mayorDebito] == 0 && cantidades[mayorCredito] == 0){
				return;
			}
			//Lo mismo que en el BalanceadorLista
			decimal cantidad = Math.min(cantidades[mayorDebito], -cantidades[mayorCredito]); 
			cantidades[mayorDebito] -= cantidad;
			cantidades[mayorCredito] += cantidad;
			balancearRec(cantidades);
		}
		//Utilidad que retorna el indice del menor valor
		public int minimo(decimal[] vector){
			int indice = 0;
			for (int i=1; i<N; i++){
				if (vector[i] < vector[indice]){
					indice = i;
				}
			}
			return indice;
		}
		//Utilidad que retorna el indice del mayor valor
		public int maximo(decimal[] vector){
			int indice = 0;
			for (int i=1; i<N; i++){
				if (vector[i] > vector[indice]){
					indice = i;
				}
			}
			return indice;
		}				
	}
}
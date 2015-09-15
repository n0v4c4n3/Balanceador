//Gaby RM - BalanceadorMatriz (14/09/2015)
using System;
using System.Collections;
namespace BalanceadorMatriz
{
	public class Utilidades{
		//Numero de vertices "personas"
		private int N { get; set; }
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
		public void balancear(decimal[][] matriz){
			decimal[N] cantidades = {0};
			for (int p=0; p<N; p++){
				for (int i=0; i<N; i++){
					//Simplificar las deudas recíprocas
					//Lo que "i" le debe a "p" menos lo que "p" acreditó a "i"
					cantidades[p] += (matriz[i][p] - matriz[p][i]);
				}
			}
			balancearRec(cantidades);
		}		
		public void balancearRec(decimal[] cantidades){
			decimal mayorCredito = maximo(cantidades); 
			decimal mayorDebito = minimo(cantidades);
			if (cantidades[mayorCredito] == 0 && cantidades[mayorDebito] == 0){
				return;
			}
			//Lo mismo que en el BalanceadorLista
			decimal cantidad = Math.min(cantidades[mayorCredito], -cantidades[mayorDebito]); 
			cantidades[mayorCredito] -= cantidad;
			cantidades[mayorDebito] += cantidad;
			balancearRec(cantidades);
		}		
	}
}
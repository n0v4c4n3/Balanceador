//Gaby RM - BalanceadorLista (13/09/2015)
using System;
using System.Collections;
namespace BalanceadorLista
{
	public class Persona{
		private string Nombre { get; set; }
		private decimal debe { get; set; }
	}
	public class Deuda{
		private Persona deudor { get; set; }
		private Persona acreedor { get; set; }
		private decimal cantidad { get; set; }
	}
	public class Utilidades{
		//Dadas las personas y deudas que se necesitan balancear 
		//Sabiendo que las personas no tenian deudas previas a estas
		private void balancear(List<Persona> personas, List<Deuda> deudas){
			//lista (ordenada) de deudores y un stack (LIFO) de acreedores
			private List<Persona> deudores = new List<Persona>(); //Lista ordenada
			private Stack<Persona> acreedores = new Stack<Persona>(); //LIFO 
			//Simplificar las deudas recíprocas
			//Se inicializa los "debe" de cada acreedor o deudor, restando y sumando según corresponda
			foreach(Deuda deuda in deudas){
				deuda.acreedor.debe -= deuda.cantidad;
				deuda.deudor.debe += deuda.cantidad;
			}
			//Si la persona termina con "debe" mayor que cero se agrega a los deudores
			//Si la persona termina con "debe" menor que cero se agrega a los acreedores 
			//Si es cero se ignora ya que se pago directamente, caso 1-1
			foreach(Persona persona in personas){
				if (persona.debe > 0){
					deudores.push(persona);
				}
				else if (persona.debe < 0){
					acreedores.push(persona);
				}
			}
			//Se ordenan descendentemente los deudores 
			//Desde el que debe más al que debe menos, Ej: Gaby $50, Carol $40, Facu $10
			deudores.OrderByDescending(deudor => deudor.debe); //Expresión lambda para ordenar deudores según deuda
			//Se ordenan ascendentemente los acreedores, notese que el "debe" de estos son siempre negativos 
			//Desde el que le deben más al que le deben menos, Ej: Flo $-60, Fer $-40
			acreedores.OrderBy(acreedor => acreedor.debe); //Expresión lambda para ordenar acreedores según deuda
			//Recorremos todos los deudores
			foreach(Persona deudor in deudores){
				//Hasta que el deudor deba 0
				while(deudor.debe > 0){
					//Retorna el acreedor al que le deben más
					//Ej1: Fer $-40
					//Ej2: Flo $-60
					//Ej3: Fer $-40
				  Persona acreedor = acreedores.peek; 
				  //La cantidad que sea menos entre 
				  //Ej1 (deudor.debe = -acreedor.debe): C1 = Math.min(Carol $40, Fer $40) = $40
				  //Ej2 (deudor.debe < -acreedor.debe): C2 = Math.min(Gaby $50, Flo $60) = $50 
				  //Ej3 (deudor.debe > -acreedor.debe): C3 = Math.min(Gaby $50, Fer $40) = $40
				  decimal cantidad = Math.min(deudor.debe, -acreedor.debe); 
				  //Ej1: Fer $-40 + C1 = $0 
				  //Ej2: Flo $-60 + C2 = $-10
				  //Ej3: Fer $-40 + C3 = $0
				  acreedor.debe += cantidad;
				  //Ej1: Carol $40 - C1 = $0 
				  //Ej2: Gaby $50 - C2 $50 = $0 
				  //Ej3: Gaby $50 - C3 $40 = $10 
				  deudor.debe -= cantidad;
			    if(acreedor.debe == 0){ //Si justo da 0 el acreedor fue pagado, ya no me interesa (se podría agregar un offset tipo +-0.50)
			      acreedores.pop //Elimina del stack
			    }  
			    //Así sucesivamente hasta que se logre balancear todas las deudas 
			    Console.WriteLine(deudor.nombre + " debe a " + acreedor.nombre + " $ " + cantidad); //Para ver como se va calculando
			  }
			}
		}
	}
}

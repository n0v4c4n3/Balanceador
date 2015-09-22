//Gaby RM - BalanceadorListaColaborativo (20/09/2015)
using System;
using System.Collections;
namespace BalanceadorLista
{
	public class Persona{
		private string Nombre { get; set; }
		private decimal debe { get; set; }
		private List<DeudaPrevia> deudasPrevias { get; set; }
		//Método para verificar si la persona "deudor" tiene deuda con un acreedor dado
		public bool tieneDeudaCon(Persona acreedor, decimal cantidad){
			foreach(DeudaPrevia dP in acreedor.deudasprevias){
				if(dP.a.equals(this) && dp.cantidad > 0 && dP.cantidad > cantidad){
					return true; //Existe deuda previa con el acreedor
				}
			}
			return false; //No existe
		}
		//Método para verificar si algún acreedor tiene deuda con ésta persona
		public void saldarDeudaCon(Persona acreedor, decimal cantidad){
			foreach(DeudaPrevia dP in acreedor.deudasprevias){
				if(dP.a.equals(this) && dP.cantidad > 0 && dP.cantidad > cantidad){
					dP.cantidad -= cantidad;
				}
			}
		}		
	}
	public class Deuda{
		private Persona deudor { get; set; }
		private Persona acreedor { get; set; }
		private decimal cantidad { get; set; }
	}
	public class DeudaPrevia{
		private Persona a { get; set; }
		private decimal cantidad { get; set; }
	}
	public class Utilidades{
		//Dadas las personas y deudas que se necesitan balancear
		private void balancear(List<Persona> personas, List<Deuda> deudas){
			//lista (ordenada) de deudores y un stack (LIFO) de acreedores
			private List<Persona> deudores = new List<Persona>(); 
			private Stack<Persona> acreedores = new Stack<Persona>(); 
			//Simplificar las deudas recíprocas
			foreach(Deuda deuda in deudas){
				deuda.acreedor.debe -= deuda.cantidad;
				deuda.deudor.debe += deuda.cantidad;
				if(deuda.deudor.tieneDeudaCon(deuda.acreedor, cantidad)){  	
				  deuda.deudor.saldarDeudaCon(deuda.acreedor, deuda.cantidad);
				  //Lo opuesto a lo anterior, por ahora... TBD pensar si funca o no
					deuda.acreedor.debe += deuda.cantidad; 
					deuda.deudor.debe -= deuda.cantidad; 
				}		
			}
			//Si la persona termina con "debe" > 0 es deudor, < 0 es acreedor y 0 se ignora
			foreach(Persona persona in personas){
				if (persona.debe > 0){
					deudores.add(persona);
				}
				else if (persona.debe < 0){
					acreedores.push(persona);
				}
			}
			//Se ordenan descendentemente los deudores 
			deudores.OrderByDescending(deudor => deudor.debe);
			//Se ordenan ascendentemente los acreedores, notese que el "debe" de estos son siempre negativos 
			acreedores.OrderBy(acreedor => acreedor.debe); 
			//Recorremos todos los deudores
			foreach(Persona deudor in deudores){
				//Hasta que el deudor deba 0
				while(deudor.debe > 0){
				  Persona acreedor = acreedores.peek(); 
				  decimal cantidad = Math.min(deudor.debe, -acreedor.debe); 
 				  deudor.debe -= cantidad;
				  acreedor.debe += cantidad;
			    if(acreedor.debe == 0){ //Si justo da 0 el acreedor fue pagado
			      acreedores.pop() //Elimina del stack
			    }
			    //Agrego las "movimientos" que actuarán como deudas previas
				  DeudaPrevia deudaPrevia = new DeudaPrevia();
				  deudaPrevia.a = acreedor;
				  deudor.deudasPrevias.add(deuda);	
			    Console.WriteLine(deudor.nombre + " tiene una deuda con " + acreedor.nombre + " $ " + cantidad); //Para ver como se va calculando
			  }
			}
		}
	}
}
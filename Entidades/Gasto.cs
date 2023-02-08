namespace Entidades
{
    public class Gasto
    {
        public int idGasto { get; set; }
        public double dblMontoGasto { get; set; }
        public DateTime dtFechaGasto { get; set; }
        public string? strDescripcionGasto { get; set; }
        public string? strNombreGasto { get; set; }
        
        public void descuentaDineroSueldo()        
        {
        }
    
        public void abonaDineroAhorro()        
        {
        }
    }

    
}
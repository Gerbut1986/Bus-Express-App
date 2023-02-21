namespace Transfer_App.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 55 Places:
    /// </summary>
    [Table("SchemaPlaces")]
    public class SchemaPlace
    {
        public int Id { get; set; }
        public System.DateTime GoDate { get; set; }
        public string BusNameNumber { get; set; }
        public bool Is1Place { get; set; } = false;
        public bool Is2Place { get; set; } = false;
        public bool Is3Place { get; set; } = false;
        public bool Is4Place { get; set; } = false;
        public bool Is5Place { get; set; } = false;
        public bool Is6Place { get; set; } = false;
        public bool Is7Place { get; set; } = false;
        public bool Is8Place { get; set; } = false;
        public bool Is9Place { get; set; } = false;
        public bool Is10Place { get; set; } = false;
        public bool Is11Place { get; set; } = false;
        public bool Is12Place { get; set; } = false;
        public bool Is13Place { get; set; } = false;
        public bool Is14Place { get; set; } = false;
        public bool Is15Place { get; set; } = false;
        public bool Is16Place { get; set; } = false;
        public bool Is17Place { get; set; } = false;
        public bool Is18Place { get; set; } = false;
        public bool Is19Place { get; set; } = false;
        public bool Is20Place { get; set; } = false;
        public bool Is21Place { get; set; } = false;
        public bool Is22Place { get; set; } = false;
        public bool Is23Place { get; set; } = false;
        public bool Is24Place { get; set; } = false;
        public bool Is25Place { get; set; } = false;
        public bool Is26Place { get; set; } = false;
        public bool Is27Place { get; set; } = false;
        public bool Is28Place { get; set; } = false;
        public bool Is29Place { get; set; } = false;
        public bool Is30Place { get; set; } = false;
        public bool Is31Place { get; set; } = false;
        public bool Is32Place { get; set; } = false;
        public bool Is33Place { get; set; } = false;
        public bool Is34Place { get; set; } = false;
        public bool Is35Place { get; set; } = false;
        public bool Is36Place { get; set; } = false;
        public bool Is37Place { get; set; } = false;
        public bool Is38Place { get; set; } = false;
        public bool Is39Place { get; set; } = false;
        public bool Is40Place { get; set; } = false;
        public bool Is41Place { get; set; } = false;
        public bool Is42Place { get; set; } = false;
        public bool Is43Place { get; set; } = false;
        public bool Is44Place { get; set; } = false;
        public bool Is45Place { get; set; } = false;
        public bool Is46Place { get; set; } = false;
        public bool Is47Place { get; set; } = false;
        public bool Is48Place { get; set; } = false;
        public bool Is49Place { get; set; } = false;
        public bool Is50Place { get; set; } = false;
        public bool Is51Place { get; set; } = false;
        public bool Is52Place { get; set; } = false;
        public bool Is53Place { get; set; } = false;
        public bool Is54Place { get; set; } = false;
        public bool Is55Place { get; set; } = false;

        public override string ToString()
        {
            return $"{Id}" +
                   $"{Is1Place}{Is2Place}{Is3Place}{Is4Place}{Is5Place}{Is6Place}" +
                   $"{Is7Place}{Is8Place}{Is9Place}{Is10Place}{Is11Place}{Is12Place}" +
                   $"{Is13Place}{Is14Place}{Is15Place}{Is16Place}{Is17Place}{Is18Place}" +
                   $"{Is19Place}{Is20Place}{Is21Place}{Is22Place}{Is23Place}{Is24Place}" +
                   $"{Is25Place}{Is26Place}{Is27Place}{Is28Place}{Is29Place}{Is30Place}" +
                   $"{Is31Place}{Is32Place}{Is33Place}{Is34Place}{Is35Place}{Is36Place}" +
                   $"{Is37Place}{Is38Place}{Is39Place}{Is40Place}{Is41Place}{Is42Place}" +
                   $"{Is43Place}{Is44Place}{Is45Place}{Is46Place}{Is47Place}{Is48Place}" +
                   $"{Is49Place}{Is50Place}{Is51Place}{Is52Place}{Is53Place}{Is54Place}" +
                   $"{Is55Place}";
        }
    }
}

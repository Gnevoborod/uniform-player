using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using uniform_player.Domain.Models;

namespace uniform_player.Database.Entities
{
    [Table("scenario")]
    public class ScenarioEntity
    {
        [Key, Column("scenario_id")]
        public int Id { get; set; }
        [Column("name"), MaxLength(256)]
        public string Name { get; set; } = default!;

        [Column("body", TypeName = "jsonb")]
        public string? Body { get; set; } = default!;

        [Column("state")]
        public ScenarioState ScenarioState { get; set; }
    }
}

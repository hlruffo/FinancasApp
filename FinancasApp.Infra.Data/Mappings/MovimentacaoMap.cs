using FinancasApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasApp.Infra.Data.Mappings
{
    /// <summary>
    /// Mapeamento da entidade Movimentacao
    /// </summary>
    public class MovimentacaoMap : IEntityTypeConfiguration<Movimentacao>
    {
        public void Configure(EntityTypeBuilder<Movimentacao> builder)
        {
            //nome da tabela
            builder.ToTable("MOVIMENTACAO");

            //chave primária
            builder.HasKey(m => m.Id);

            //campos da entidade
            builder.Property(m => m.Id)
                .HasColumnName("ID");

            //campos da entidade
            builder.Property(m => m.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(m => m.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(m => m.Data)
                .HasColumnName("DATA")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(m => m.Valor)
                .HasColumnName("VALOR")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(m => m.UsuarioId)
                .HasColumnName("USUARIOID")
                .IsRequired();

            builder.Property(m => m.CategoriaId)
                .HasColumnName("CATEGORIAID")
                .IsRequired();


            //relacionamento com usuario
            builder.HasOne(m => m.Usuario)       // Define a relação com a entidade Usuario
                .WithMany(u => u.Movimentacoes)   // Indica que um usuário pode ter várias movimentações
                .HasForeignKey(m => m.UsuarioId)  // Especifica a chave estrangeira
                .OnDelete(DeleteBehavior.NoAction);


            //relacionamento com categoria
            builder.HasOne(m => m.Categoria)       // Define a relação com a entidade Categoria
                .WithMany(c => c.Movimentacoes)     // Indica que uma categoria pode ter várias movimentações
                .HasForeignKey(m => m.CategoriaId) // Especifica a chave estrangeira
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VideoAPI.Infrastructure;

namespace VideoAPI.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190924171722_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("VideoAPI.app.models.Document", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AssetName")
                        .HasColumnName("assetname")
                        .HasColumnType("text");

                    b.Property<string>("EncodeJobName")
                        .HasColumnName("encodejobname")
                        .HasColumnType("text");

                    b.Property<string>("EncodedAssetName")
                        .HasColumnName("encodedassetname")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .HasColumnName("filename")
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .HasColumnName("path")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("document","novus");
                });

            modelBuilder.Entity("VideoAPI.app.models.DocumentStreamingUrl", b =>
                {
                    b.Property<long>("DocumentId")
                        .HasColumnName("documentid")
                        .HasColumnType("bigint");

                    b.Property<long>("StreamingUrlId")
                        .HasColumnName("streamingurlid")
                        .HasColumnType("bigint");

                    b.HasKey("DocumentId", "StreamingUrlId");

                    b.HasIndex("StreamingUrlId");

                    b.ToTable("document_streamingurl","novus");
                });

            modelBuilder.Entity("VideoAPI.app.models.StreamingUrl", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("StreamingProtocol")
                        .HasColumnName("streamingprotocol")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnName("url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("streamingurl","novus");
                });

            modelBuilder.Entity("VideoAPI.app.models.DocumentStreamingUrl", b =>
                {
                    b.HasOne("VideoAPI.app.models.Document", "Document")
                        .WithMany("StreamingUrls")
                        .HasForeignKey("DocumentId")
                        .HasConstraintName("document_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VideoAPI.app.models.StreamingUrl", "StreamingUrl")
                        .WithMany("Documents")
                        .HasForeignKey("StreamingUrlId")
                        .HasConstraintName("streamingurl_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vets.Models;

namespace Vets.Data
{
    /// <summary>
    /// classe para representar a base de dados do Projeto
    /// equivalente a escrever o comando "CREATE DATABASE VetsDB"
    /// </summary>
    public class VetsDB : DbContext
    {

        /// <summary>
        /// Construtor da classe
        /// server para ligar esta classe à BD
        /// </summary>
        /// <param name="options"></param>
        public VetsDB(DbContextOptions<VetsDB> options) : base(options) { }


        // adicionar as 'tabelas' à BD
        public DbSet<Animais> Animais { get; set; }
        public DbSet<Donos> Donos { get; set; }
        public DbSet<Veterinarios> Veterinarios { get; set; }
        public DbSet<Consultas> Consultas { get; set; }


    }
}
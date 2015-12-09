using System;
using System.Collections.Generic;
using System.Data;
using Yupi.Data.Base.Sessions.Interfaces;
using Yupi.Game.Pets.Structs;

namespace Yupi.Game.Pets
{
    /// <summary>
    ///     Class PetCommandHandler.
    /// </summary>
    internal class PetCommandHandler
    {
        /// <summary>
        ///     The Table
        /// </summary>
        private static DataTable _table;

        /// <summary>
        ///     The _pet commands
        /// </summary>
        private static Dictionary<string, PetCommand> _petCommands;

        /// <summary>
        ///     Gets the pet commands.
        /// </summary>
        /// <param name="pet">The pet.</param>
        /// <returns>Dictionary&lt;System.Int16, System.Boolean&gt;.</returns>
        internal static Dictionary<short, bool> GetPetCommands(Pet pet)
        {
            var output = new Dictionary<short, bool>();
            var qLevel = (short) pet.Level;

            switch (pet.Type)
            {
                case 0: // Cachorro
                case 1: // Gato
                case 2: // Crocodilo
                case 3: // Terrier
                case 4: // Urso
                case 5: // Porco
                case 6: // Leão
                case 7: // Rinoceronte
                    output.Add(0, true); // Sentar
                    output.Add(1, true); // Descansar
                    output.Add(13, true); // Deitar
                    output.Add(2, qLevel >= 2); // Deitar
                    output.Add(4, qLevel >= 3); // Pedir
                    output.Add(3, qLevel >= 4); // Seguir
                    output.Add(5, qLevel >= 4); // Fingir de Morto
                    output.Add(43, qLevel >= 5); // Comer
                    output.Add(14, qLevel >= 5); // Beber
                    output.Add(6, qLevel >= 6); // Silêncio
                    output.Add(17, qLevel >= 6); // Futebol
                    output.Add(8, qLevel >= 8); // Em pé
                    output.Add(7, qLevel >= 9); // Seguir
                    output.Add(9, qLevel >= 11); // Pular
                    output.Add(11, qLevel >= 11); // Brincar
                    output.Add(12, qLevel >= 12); // Quieto
                    output.Add(10, qLevel >= 12); // Falar
                    output.Add(15, qLevel >= 16); // Ir para Esquerda
                    output.Add(16, qLevel >= 16); // Ir para Direita
                    output.Add(24, qLevel >= 17); // Ir para Frente

                    if (pet.Type == 3 || pet.Type == 4)
                        output.Add(46, true); // Breed
                    break;

                case 8: // Aranha
                    output.Add(1, true); // Descansar
                    output.Add(2, true); // Teia
                    output.Add(3, qLevel >= 2); // Seguir
                    output.Add(17, qLevel >= 3); // Futebol
                    output.Add(6, qLevel >= 4); // Silêncio
                    output.Add(5, qLevel >= 4); // Fingir de Morto
                    output.Add(7, qLevel >= 5); // Seguir
                    output.Add(23, qLevel >= 6); // Botar Fogo
                    output.Add(9, qLevel >= 7); // Saltar
                    output.Add(10, qLevel >= 8); // Falar
                    output.Add(11, qLevel >= 8); // Jogar
                    output.Add(24, qLevel >= 9); // Ir para frente
                    output.Add(15, qLevel >= 10); // Ir para Esquerda
                    output.Add(16, qLevel >= 10); // Ir para Direita
                    output.Add(13, qLevel >= 12); // Ir para Casa
                    output.Add(14, qLevel >= 13); // Beber
                    output.Add(19, qLevel >= 14); // Botar
                    output.Add(20, qLevel >= 14); // Estátua
                    output.Add(22, qLevel >= 15); // Girar
                    output.Add(21, qLevel >= 16); // Dançar
                    break;

                case 16: // Que tipo é?
                    break;
            }

            return output;
        }

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal static void Init(IQueryAdapter dbClient)
        {
            dbClient.SetQuery("SELECT * FROM pets_commands");

            _table = dbClient.GetTable();
            _petCommands = new Dictionary<string, PetCommand>();

            foreach (DataRow row in _table.Rows)
                _petCommands.Add(row[1].ToString(),  new PetCommand(Convert.ToInt32(row[0].ToString()), row[1].ToString()));
        }

        /// <summary>
        ///     Tries the invoke.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.Int32.</returns>
        internal static int TryInvoke(string input)
        {
            PetCommand command;
            return _petCommands.TryGetValue(input, out command) ? command.CommandId : 0;
        }
    }
}
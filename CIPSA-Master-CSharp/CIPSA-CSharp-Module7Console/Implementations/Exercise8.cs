using System;
using System.Collections;
using System.Drawing;
using CIPSA_CSharp_Common;
using Console = Colorful.Console;

namespace CIPSA_CSharp_Module7Console.Implementations
{
    public class Exercise8
    {
        private readonly string SIZE = "Cantidad de estudiantes";
        private readonly string APPROVED = "Cantidad de aprobados";
        private readonly string SUSPENSE = "Cantidad de suspensos";
        public void ExecuteExercise()
        {
            //En este ejercicio asumo que el suspenso es del 1 al 5 incluyendo y el aprobado, pues del 6-10
            var hashNotes = new Hashtable();
            LoadData(hashNotes);
            Console.WriteLine("Escriba las notas de los estudiantes");
            SaveNotesOfUser(hashNotes);
            var size = (int) hashNotes[SIZE];
            if (size > 0)
            {
                var approved = (int)hashNotes[APPROVED];
                var suspense = (int)hashNotes[SUSPENSE];
                var average = GetAverageTotal(hashNotes);
                var approvedMessage = "No tiene estudiantes aprobados";
                var suspenseMessage = "No tiene estudiantes suspensos";

                if (approved > 0)
                {
                    var averageApproved = GetAverageApproved(hashNotes);
                    approvedMessage = $"El promedio de aprobados de notas es: {averageApproved}, " +
                                      $"siendo un total de {approved} estudiante(s) aprobado(s)";
                }

                if (suspense > 0)
                {
                    var averageSuspense = GetAverageSuspense(hashNotes);
                    suspenseMessage = $"El promedio de suspensos de notas es: {averageSuspense}, " +
                                      $"siendo un total de {suspense} estudiante(s) suspenso(s)";
                }

                Console.WriteLine($"La cantidad de estudiantes aprobados es: {approved}", Color.DarkGreen);
                Console.WriteLine($"La cantidad de estudiantes suspensos es: {suspense}", Color.DarkGreen);
                Console.WriteLine($"El promedio del total de notas es: {average}, siendo un total de {size} estudiante(s)", Color.DarkGreen);
                Console.WriteLine(approvedMessage, Color.DarkGreen);
                Console.WriteLine(suspenseMessage, Color.DarkGreen);
            }
            else
            {
                Console.WriteLine("Lo sentimos, no tiene ninguna nota", Color.DarkRed);
            }



        }

        private int GetAverageTotal(Hashtable hashNotes)
        {
            return ((int)hashNotes[1] * 1 +
                   (int)hashNotes[2] * 2 +
                   (int)hashNotes[3] * 3 +
                   (int)hashNotes[4] * 4 +
                   (int)hashNotes[5] * 5 +
                   (int)hashNotes[6] * 6 +
                   (int)hashNotes[7] * 7 +
                   (int)hashNotes[8] * 8 +
                   (int)hashNotes[9] * 9 +
                   (int)hashNotes[10] * 10) / (int)hashNotes[SIZE];
        }

        private int GetAverageApproved(Hashtable hashNotes)
        {
            return ((int)hashNotes[6] * 6 +
                   (int)hashNotes[7] * 7 +
                   (int)hashNotes[8] * 8 +
                   (int)hashNotes[9] * 9 +
                   (int)hashNotes[10] * 10) / (int)hashNotes[APPROVED];
        }

        private int GetAverageSuspense(Hashtable hashNotes)
        {
            return ((int)hashNotes[1] * 1 +
                   (int)hashNotes[2] * 2 +
                   (int)hashNotes[3] * 3 +
                   (int)hashNotes[4] * 4 +
                   (int)hashNotes[5] * 5) / (int)hashNotes[SUSPENSE];
        }

        //private int GetQuantityOfApproved(Hashtable hashNotes)
        //{
        //    return (int) hashNotes[6] +
        //           (int) hashNotes[7] +
        //           (int) hashNotes[8] +
        //           (int) hashNotes[9] +
        //           (int) hashNotes[10];
        //}

        //private int GetQuantityOfSuspense(Hashtable hashNotes)
        //{
        //    return (int) hashNotes[1] +
        //           (int) hashNotes[2] +
        //           (int) hashNotes[3] +
        //           (int) hashNotes[4] +
        //           (int) hashNotes[5];
        //}

        private void SaveNotesOfUser(Hashtable hashNotes)
        {
            while (true)
            {
                Console.WriteLine("Diga una nota (1-10)");
                var value = Console.ReadLine();
                if (value != null && value.Equals(string.Empty))
                {
                    break;
                }
                if (Helper.GetNumeric(value) != -1 && Convert.ToInt32(value) > 0 && Convert.ToInt32(value) <= 10)
                {
                    hashNotes[Convert.ToInt32(value)] = (int)hashNotes[Convert.ToInt32(value)] + 1;
                    hashNotes[SIZE] = (int)hashNotes[SIZE] + 1;
                    if (Convert.ToInt32(value) > 5)
                    {
                        hashNotes[APPROVED] = (int)hashNotes[APPROVED] + 1;
                    }
                    else
                    {
                        hashNotes[SUSPENSE] = (int)hashNotes[SUSPENSE] + 1;
                    }
                }
                
            }
        }

        private void LoadData(Hashtable hashNotes)
        {
            hashNotes.Add(SIZE,0);
            hashNotes.Add(APPROVED,0);
            hashNotes.Add(SUSPENSE,0);
            hashNotes.Add(1,0);
            hashNotes.Add(2,0);
            hashNotes.Add(3,0);
            hashNotes.Add(4,0);
            hashNotes.Add(5,0);
            hashNotes.Add(6,0);
            hashNotes.Add(7,0);
            hashNotes.Add(8,0);
            hashNotes.Add(9,0);
            hashNotes.Add(10,0);
        }
    }
}

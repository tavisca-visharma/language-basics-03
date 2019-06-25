using System;
using System.Linq;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        static HashSet<int> calculateMax(int[] a, HashSet<int> list){
            int max = -999;
            HashSet<int> newList = new HashSet<int>();
            foreach(var i in list){
                max = Math.Max(max, a[i]);
            }
            foreach(var i in list){
                if(a[i] == max){
                    newList.Add(i);
                }
            }
            return newList;
        }

        static HashSet<int> calculateMin(int[] a, HashSet<int> list){
            int min = 999;
            HashSet<int> newList = new HashSet<int>();
            foreach(var i in list){
                min = Math.Min(min, a[i]);
            }
            foreach(var i in list){
                if(a[i] == min){
                    newList.Add(i);
                }
            }
            return newList;
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int[] output = new int[dietPlans.Length];
            /*
                An array that will store the respective calorie value for  each index.
            */
            int[] calorie = new int[protein.Length];
            for(int i=0; i<protein.Length; i++){
                calorie[i] = fat[i]*9 + carbs[i]*5 + protein[i]*5;
            }
            for(int i=0; i<dietPlans.Length; i++){
                /*
                    For each new dietPlan create a list/set, initially containing all the elements indices.
                */
                HashSet<int> set = new HashSet<int>();
                for(int j=0; j<protein.Length; j++){
                    set.Add(j);
                }

                for(int j=0; j<dietPlans[i].Length; j++){
                    /*
                        Idea :- 
                        1.) For each character in the dietPlan, calculate respective indices which contains
                            maximum/minimum.
                        2.) If there is not any tie, then return the corresponding index.
                        3.) If there is a tie then, restrict the search in the list of indicies obtaine, and perform
                            the respective calculation for another charcter in the dietplan.
                        4.) Finally choose the least valid index.
                    */
                    if(dietPlans[i][j] == 'F'){
                        set = calculateMax(fat, set);
                    }else if(dietPlans[i][j] == 'f'){
                        set = calculateMin(fat, set);
                    }else if(dietPlans[i][j] == 'P'){
                        set = calculateMax(protein, set);
                    }else if(dietPlans[i][j] == 'p'){
                        set = calculateMin(protein, set);
                    }else if(dietPlans[i][j] == 'C'){
                        set = calculateMax(carbs, set);
                    }else if(dietPlans[i][j] == 'c'){
                        set = calculateMin(carbs, set);
                    }else if(dietPlans[i][j] == 'T'){
                        set = calculateMax(calorie, set);
                    }else if(dietPlans[i][j] == 't'){
                        set = calculateMin(calorie, set);
                    }else{

                    }
                    
                    if(set.Count == 1){
                        break;
                    }

                }

                /*
                    Choose only the first index among the set/list.
                */
                int value = -1;
                foreach(var num in set){
                    value = num;
                    break;
                }
                output[i] = value;
            }

            return output;
        }
    }
}

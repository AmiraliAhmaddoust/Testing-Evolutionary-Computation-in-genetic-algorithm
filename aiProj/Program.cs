using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aiProj
{
    class Program
    {
      public  static Random generator = new Random();
        static void Main(string[] args)
        {
          //  Random generator = new Random();
           
            int[] zhen = new int[50];
            List<member> members = new List<member>();

            int v=50;    // n vazir
            int n=40;    //nasl
            int z=50;   //zhen haye har nasl



            for (int j = 0; j < v; j++)
            {
                for (int i = 0; i < z; i++)//vase sakhtan 100 nasel
                {
                    int a = generator.Next(0, v);
                    zhen[i] = a;


                }
                member m = new member(zhen);
                m.count_fitnet();
                members.Add(m);
            }
            //bara tartib
            //for (int j = 0; j < v; j++)
            //{
            //    for (int i = 0; i < z; i++)//vase sakhtan 100 nasel
            //    {
            //        int a = generator.Next(0, v);
            //        if (!zhen.Contains(a))
            //        {
            //            zhen[i] = a;
            //        }
            //        else
            //        {
            //            i = i - 1;
            //        }
            //    }
            //}
            //member m = new member(zhen);
            //m.count_fitnet();
            //members.Add(m);




            queen sovle =new queen(members);
            List<int> keep_fitnet = new List<int>();
            for(int i = 0; i < n; i++)
            {
                sovle.eslah();
               sovle.reghabati();
               //   sovle.chrkhRolet();
                
                //  sovle.crosover_siglePoint();
              //  sovle.crossover_tartib();
                  sovle.crossover_yknavakht();
                sovle.eslah();
                sovle.mutation_standard();
            //    sovle.mutation_taviz();
                sovle.final_fitnet();
                keep_fitnet.Add(sovle.fitnet);
                
            }
            
            sovle.best_anw();
            int be = sovle.best;
            member m2 = new member(sovle.members[be].ozve);
            
            m2.fitnet = sovle.members[be].fitnet;

            Console.WriteLine("minagin fitnet ha");
            for (int i = 0; i < keep_fitnet.Count(); i++)
            {

                Console.Write(keep_fitnet[i] + ",");
            }
            Console.WriteLine();
            Console.WriteLine("best ansewr");
            for (int i = 0; i < m2.ozve.Length; i++)
            {

                Console.Write(m2.ozve[i] + " ,");
            }
            Console.WriteLine("");
            Console.Write(m2.fitnet);


            Console.ReadKey();


        }
    }
    class member
    {
       // Random generate = new Random();
        public int[] ozve ;
        public int fitnet;
        public double shance_rolet;
        
       public member() { }
        public member(int[] ozve )
        { 
            this.ozve = new int[50];
            for(int i = 0; i < ozve.Length; i++)
            {
                this.ozve[i] = ozve[i];
            }

        }
        public void count_fitnet()
        {
            fitnet = 0;
            for (int i = 0; i < ozve.Length - 1; i++)
            {
                for (int j = i + 1; j < ozve.Length; j++)
                {
                    if (Math.Abs(ozve[i] - i) != Math.Abs(ozve[j] - j) )
                    {
                        if (ozve[i] != ozve[j])
                        {
                            fitnet++;
                        }
                    }
                }
            }

        }
        public void exchange(member m,int i)
        {
            
            for (int j = 0; j < i; j++) {
              //  int a = ozve[j];
                ozve[j] = m.ozve[j];

            }
        }
        public void hal()
        {

            for (int i = 0; i < 50; i++)
            {


                if (ozve[i] > 50)
                {
                    ozve[i] = Program.generator.Next(0, 50);
                    for (int j = 0; j < 50; j++)
                    {
                        if (j == i)
                        {
                            continue;
                        }
                        else if (ozve[i] == ozve[j])
                        {
                            ozve[i] = Program.generator.Next(0, 50);
                            j = -1;
                        }

                    }

                }


                if (ozve[i] < 0)
                {
                    ozve[i] = Program.generator.Next(0, 50);
                    for (int j = 0; j < 50; j++)
                    {
                        if (j == i)
                        {
                            continue;
                        }
                        else if (ozve[i] == ozve[j])
                        {
                            ozve[i] = Program.generator.Next(0, 50);
                            j = -1;
                        }

                    }
                }

            }
        }

    }
    class queen
    {
      public  int best;
        //Random generator = new Random();
        public List<member> members = new List<member>();
        public  int fitnet = 0;
        public queen(List<member> members)
        {
            
            this.members = members;

        }


        public void reghabati()
        {
            List<member> reghabet = new List<member>();
            int[] chance = new int[5];
            for (int tekrar = 0; tekrar < 50; tekrar++)
            {
                for (int i = 0; i < 5; i++)
                {
                    chance[i] = Program.generator.Next(0, 50);
                }
                member max = members[chance[0]];
                for (int i = 1; i < 5; i++)
                {


                    if (members[chance[i]].fitnet > max.fitnet)
                    {
                        max = members[chance[i]];
                    }

                }
                reghabet.Add(max);
            }
            members = reghabet;
        }
        public void crosover_siglePoint()
        {
            int mother;
            int father;
            //  crossover_point = 40;
            //har parent 2 farzand jaye khodeshon
            List<int> delete_tekrar = new List<int>();

            for (int i = 0; i < 15; i++)//40 ta crossovwer ke 1 father 1 mom pas 20 par tekrar
            {

                
                father = Program.generator.Next(0, 50);
                delete_tekrar.Add(father);
                do
                {
                    mother = Program.generator.Next(0, 50);
                } while (father == mother );
                delete_tekrar.Add(mother);


                int[] a = new int[50];
                for (int d = 0; d < 50; d++)
                {
                    a[d] = members[mother].ozve[d];
                }
                member first = new member(a);

                int[] b = new int[50];
                for (int d = 0; d < 50; d++)
                {
                    b[d] = members[father].ozve[d];
                }
                member second = new member(b);

                int[] c = new int[50];
                for (int d = 0; d < 50; d++)
                {
                    c[d] = members[father].ozve[d];
                }
                member cc = new member(c);



                second.exchange(first, 50);
                first.exchange(cc, 50);
                members[father] = second;
                members[father].count_fitnet();
                members[mother] = first;
                members[mother].count_fitnet();

            }
        }
        public void mutation_standard()
        {
            int memb = Program.generator.Next(0, 50);
            int jen = Program.generator.Next(0, 50);
            int change;
            do
            {
                change = Program.generator.Next(0, 50);
                members[memb].ozve[jen] += change;
            } while (members[memb].ozve[jen] + change > 50);
            members[memb].count_fitnet();


        }
        public void mutation_taviz()
        {
            int memb = Program.generator.Next(0, 50);
            int jen = Program.generator.Next(0,50);
            int jen2;
            do
            {
                jen2 = Program.generator.Next(0, 50);
            } while (jen == jen2);
            int a = jen;
            int b = jen2;
            members[memb].ozve[jen2] = a;
            members[memb].ozve[jen] = b;
            members[memb].count_fitnet();

        }
        public void chrkhRolet()
        {
            List<member> rolet = new List<member>();
            int all_fitnet = 0;
            double hlep = 0;
            for (int i = 0; i < members.Count(); i++)
            {
                all_fitnet += members[i].fitnet;
            }
            for (int i = 0; i < members.Count(); i++)
            {
                double a = Convert.ToDouble(members[i].fitnet);
                double b = Convert.ToDouble(all_fitnet);

                hlep += a / b;
                members[i].shance_rolet = hlep;

            }
            for (int j = 0; j < 50; j++)
            {
                double select = Program.generator.NextDouble();
                for (int i = 0; i < members.Count(); i++)
                {
                    if (members[i].shance_rolet > select)
                    {
                        rolet.Add(members[i]);
                        break;
                    }

                }
            }
            members = rolet;
        }
        public void eslah()
        {

            for (int k = 0; k < members.Count(); k++)
            {
                List<int> not_exist = new List<int>();
                List<int> ozve = new List<int>();

                for (int c = 0; c < members[k].ozve.Length; c++)
                {
                    ozve.Add(members[k].ozve[c]);
                }
                for (int d = 0; d < members[k].ozve.Length; d++)
                {
                    if (!ozve.Contains(d))
                    {
                        not_exist.Add(d);
                    }
                }

                for (int i = 0; i < members[k].ozve.Length - 1; i++)
                {

                    for (int j = i + 1; j < members[k].ozve.Length; j++)
                    {
                        if (members[k].ozve[i] == members[k].ozve[j])
                        {
                            members[k].ozve[j] = not_exist[0];
                            not_exist.RemoveAt(0);
                        }
                    }
                }
            }
        }
        public void crossover_tartib()
        {
            int mother;
            int father;
            List<int> aray = new List<int>();
            List<int> aray2 = new List<int>();
            for (int u = 0; u < 15; u++)
            {
                father = Program.generator.Next(0, 50);
                do
                {
                    mother = Program.generator.Next(0, 50);
                } while (father == mother);

                for (int j = 0; j < members[mother].ozve.Length; j++)
                {
                    double rand = Program.generator.NextDouble();
                    if (rand < 0.5)
                    {
                        aray.Add(members[father].ozve[j]);
                        aray2.Add(members[mother].ozve[j]);
                    }
                }
                int[] a_father = new int[aray.Count];
                int[] a_mother = new int[aray.Count];
                int[] a1_father = new int[aray.Count];
                int[] a1_mother = new int[aray.Count];
                for (int i = 0; i < aray.Count; i++)
                {
                    a_father[i] = aray[i];
                    a_mother[i] = aray2[i];
                }
                int step_zero = 0;
                int step_one = 0;

                for (int i = 0; i < members[mother].ozve.Length; i++)
                {
                    if (a_father.Contains(members[mother].ozve[i]))
                    {
                        a1_father[step_zero] = members[mother].ozve[i];
                        step_zero++;
                    }
                    if (a_mother.Contains(members[father].ozve[i]))
                    {
                        a1_mother[step_one] = members[father].ozve[i];
                        step_one++;
                    }

                }

                for (int i = 0; i < members[father].ozve.Count(); i++)
                {
                    for (int k = 0; k < a_father.Length; k++)
                    {
                        if (members[father].ozve[i] == a_father[k])
                        {
                            members[father].ozve[i] = -10;

                        }
                        if (members[mother].ozve[i] == a_mother[k])
                        {
                            members[mother].ozve[i] = -10;

                        }

                    }
                }

                int f_step = 0;
                int m_step = 0;
                for (int i = 0; i < members[father].ozve.Length; i++)
                {
                    if (members[father].ozve[i] == -10)
                    {
                        members[father].ozve[i] = a1_father[f_step];
                        f_step++;
                    }
                    if (members[mother].ozve[i] == -10)
                    {
                        members[mother].ozve[i] = a1_mother[m_step];
                        m_step++;
                    }
                }
            }

        }
        public void crossover_yknavakht()
        {
            int mother;
            int father;
            for (int u = 0; u < 15; u++)
            {
                father = Program.generator.Next(0, 50);
                do
                {
                    mother = Program.generator.Next(0, 50);
                } while (father == mother);
                int[] f = new int[members[father].ozve.Length];
                int[] m = new int[members[father].ozve.Length];
                for (int i = 0; i < members[father].ozve.Length; i++)
                {
                    f[i] = members[father].ozve[i];
                    m[i] = members[mother].ozve[i];
                }
                for (int i = 0; i < f.Length; i++)
                {
                    double rand = Program.generator.NextDouble();
                    if (rand > 0.5)
                    {
                        int help;
                        help = f[i];
                        f[i] = m[i];
                        m[i] = help;

                    }
                }
                for (int i = 0; i < members[father].ozve.Length; i++)
                {
                    members[father].ozve[i] = f[i];
                    members[mother].ozve[i] = m[i];
                }

            }
        }
        public void final_fitnet(){
       int     final_fit = 0;
        for(int i = 0; i < members.Count(); i++)
            {
                members[i].count_fitnet();
                final_fit += members[i].fitnet;
            }
            fitnet = final_fit / members.Count();
        }
        public void best_anw()
        {
            int max = members[0].fitnet;
            for (int i = 1; i < members.Count(); i++)
            {
                if (members[i].fitnet > max)
                {
                    max = members[i].fitnet;


                    best = i;
                }
            }
            members[best].hal();

        }
    }
}

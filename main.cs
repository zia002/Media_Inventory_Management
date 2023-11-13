/*
Author:Mohammad Zia
Name of Assignment:Advanced Media (Book,CD,DVD) Inventory Management
Date:13 Nov 2023
Language:C# (Console Application)
*/
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
#pragma warning disable CS8604 
#pragma warning disable CS8600
#pragma warning disable CS8601

namespace assignment{
    //---Generic class for all the common properties of media ---//
    public class Media<T,TT>{
        internal T Title;
        internal T Type;
        internal TT Release_year;
        public Media(T title,T type,TT release_year){
            this.Title=title;
            this.Type=type;
            this.Release_year=release_year;
        }
    }
    //---------Book Main Class--------------//
    public partial class Book:Media<string,int>{
        public string Author;
        public string Publication;
        public int Price;
        public Book(string author,string title,string type,int date,string publication,int price):base(title,type,date){
            this.Author=author;
            this.Publication=publication;
            this.Price=price;
        }
    }
    //---------partial class of Book--------//
    public partial class Book{
        /*
        To Delete or Update a particular item of book from the list,first we store the index
        of the Book object's in book_idx list.Then by using the stored index ,we have to update or delete items.
        */
        static List<int> book_idx=new List<int>();
        static int idx=0;
        /*
        This method will show all the book's object in the list item's
        */
        public  static void show_books(List<object> List_items){
            int cnt=1;
            System.Console.WriteLine("\n---------------All The Book Information----------------");
            foreach(var data in List_items){
                if(data is Book){
                    book_idx.Add(idx);
                    Book it=(Book)data;
                    Console.WriteLine($"{cnt}.Book:\n---Title:{it.Title}\n---Author:{it.Author}");
                    Console.WriteLine($"---Book Type:{it.Type}\n---Release Year:{it.Release_year}\n---Publication:{((Book)it).Publication}\n---Price:{it.Price}");
                    ++cnt;
                }
                ++idx;
            }
            System.Console.WriteLine("--------------------------------------------------------");
        }
        /*
        To delete a particular book choosing from the book list,first we take user 
        input  of the book Serial number,then by using the index we store in the book_idx
        list,now,we delete the particular object from the list.
        */
        public static void delete_book(List<object> List_items){
            int id,rem_idx;
            System.Console.Write("Enter the ID No of Book Want to remove:");
            id=int.Parse(Console.ReadLine());
            rem_idx=book_idx[id-1];
            List_items.RemoveAt(rem_idx);
            System.Console.WriteLine($"-------{id}.ID Book is Deleted------");
        }
        // This method will delete all the book's object's
        public static void all_del_book(List<object> List_items){
            List_items.RemoveAll(data=>data is Book);
            System.Console.WriteLine($"-------All Book's Are Deleted------");
        }
        /*
        To update a particular book choosing from the book list,first we take user 
        input  of the book Serial number,then by using the index we store in the book_idx
        list,we take input from user,which value want to change then partuar object from the list
        will be changed.
        */
        public static void upadate_book(List<object> List_items){
            int id,up_idx,option;
            System.Console.Write("Enter the ID No of Book Want to update:");
            id=int.Parse(Console.ReadLine());
            up_idx=book_idx[id-1];
            System.Console.WriteLine("-------------------------Menu-------------------------");
            System.Console.WriteLine("1.Update Author Name\n2.Update Title");
            System.Console.WriteLine("3.Update Type\n4.Update Release Year");
            System.Console.Write("Enter option:");
            option=int.Parse(Console.ReadLine());
            System.Console.WriteLine("------------------------------------------------------");
            if(option==1){
                string change;
                System.Console.Write("Enter Auther Name:");
                change=Console.ReadLine();
                ((Book)List_items[up_idx]).Author=change;
            }
            else if(option==2){
                string change;
                System.Console.Write("Enter Title Name:");
                change=Console.ReadLine();
                ((Book)List_items[up_idx]).Title=change;
            }
            else if(option==3){
                string change;
                System.Console.Write("Enter Type:");
                change=Console.ReadLine();
                ((Book)List_items[up_idx]).Type=change;
            }
            else if(option==4){
                int change;
                System.Console.Write("Enter Year:");
                change=int.Parse(Console.ReadLine());
                ((Book)List_items[up_idx]).Release_year=change;
            }
            System.Console.WriteLine($"-------{id}.ID Book information Updated------");
        }
        // this method will show the order of recent year published book's
        public static void show_book_recent_year(List<object> List_items){
            int cnt=1;
            var ans=from data in List_items where data is Book orderby ((Book)data).Release_year descending select data;
            System.Console.WriteLine("\n------------All The Book's Information------------");
            foreach(var it in ans){
                Console.WriteLine($"{cnt}.Book:\n---Title:{((Book)it).Title}\n---Author:{((Book)it).Author}");
                Console.WriteLine($"---Book Type:{((Book)it).Type}\n---Release Year:{((Book)it).Release_year}\n---Publication:{((Book)it).Publication}\n---Price:{((Book)it).Price}");
                ++cnt;
            }
            System.Console.WriteLine("------------------------------------------------------");
        }
        //This method will show The Highest price to lowest price of Book List
        public static void show_high_low(List<object> List_items){
            int cnt=1;
            var ans=from data in List_items where data is Book orderby ((Book)data).Price descending select data;
            System.Console.WriteLine("\n----All The Book's Information Price High To Low-----");
            foreach(var it in ans){
                Console.WriteLine($"{cnt}.Book:\n---Title:{((Book)it).Title}\n---Author:{((Book)it).Author}");
                Console.WriteLine($"---Book Type:{((Book)it).Type}\n---Release Year:{((Book)it).Release_year}\n---Publication:{((Book)it).Publication}\n---Price:{((Book)it).Price}");
                ++cnt;
            }
            System.Console.WriteLine("------------------------------------------------------");
        }
        //This method will show The Lowest price to Highest price of Book List
        public static void show_low_high(List<object> List_items){
            int cnt=1;
            var ans=from data in List_items where data is Book orderby ((Book)data).Price ascending select data;
            System.Console.WriteLine("\n----All The Book's Information Price Low To High-----");
            foreach(var it in ans){
                Console.WriteLine($"{cnt}.Book:\n---Title:{((Book)it).Title}\n---Author:{((Book)it).Author}");
                Console.WriteLine($"---Book Type:{((Book)it).Type}\n---Release Year:{((Book)it).Release_year}\n---Publication:{((Book)it).Publication}\n---Price:{((Book)it).Price}");
                ++cnt;
            }
            System.Console.WriteLine("------------------------------------------------------");
        }
        /*
        User will give a Book Author Name {Humayon ahmed,Kaji Nazrul Islam etc}
        then all the Book's of the Author will be shown from the list items
        */
        public static void show_by_author(List<object> List_items){
            System.Console.Write("Enter the author Name:");
            string author=Console.ReadLine();
            int cnt=1;
            var ans_author=from data in List_items where (data is Book && ((Book)data).Author==author ) select data;
            System.Console.WriteLine("\n-----------All The Book's Information-------------");
            foreach(var it in ans_author){
                Console.WriteLine($"{cnt}.Book:\n---Title:{((Book)it).Title}\n---Author:{((Book)it).Author}");
                Console.WriteLine($"---Book Type:{((Book)it).Type}\n---Release Year:{((Book)it).Release_year}\n---Publication:{((Book)it).Publication}\nPrice:{((Book)it).Price}");
                ++cnt;
            }
            System.Console.WriteLine("------------------------------------------------------");
        }
        /*
        User will give a Book type Name {Novel,poem,literature etc}
        then all the Book's of the type will be shown from the list items
        */
        public static void show_by_book_type(List<object> List_items){
            System.Console.Write("Enter the Book Type:");
            string type=Console.ReadLine();
            int cnt=1;
            var ans_type=from data in List_items where (data is Book && ((Book)data).Type==type ) select data;
            System.Console.WriteLine("\n------------All The Book's Information------------");
            foreach(var it in ans_type){
                Console.WriteLine($"{cnt}.Book:\n---Title:{((Book)it).Title}\n---Author:{((Book)it).Author}");
                Console.WriteLine($"---Book Type:{((Book)it).Type}\n---Release Year:{((Book)it).Release_year}\n---Publication:{((Book)it).Publication}\nPrice:{((Book)it).Price}");
                ++cnt;
            }
            System.Console.WriteLine("------------------------------------------------------");
        }
        /*
        User will give a Book Publication Name { etc}
        then all the Book's of the Publication will be shown from the list items
        */
        public static void show_publication(List<object> List_items){
            System.Console.Write("Enter the Book Publication Name:");
            string publication=Console.ReadLine();
            int cnt=1;
            var ans_pub=from data in List_items where (data is Book && ((Book)data).Publication==publication ) select data;
            System.Console.WriteLine($"\n---All The Book's of {publication} Publication-----");
            foreach(var it in ans_pub){
                Console.WriteLine($"{cnt}.Book:\n---Title:{((Book)it).Title}\n---Author:{((Book)it).Author}");
                Console.WriteLine($"---Book Type:{((Book)it).Type}\n---Release Year:{((Book)it).Release_year}");
                Console.WriteLine($"---Publication:{((Book)it).Publication}\nPrice:{((Book)it).Price}");
                ++cnt;
            }
            System.Console.WriteLine("------------------------------------------------------");
        }
         /*
        User will give a Book Author Name {Humayon ahmed,Kaji Nazrul Islam etc}
        then all the Book's of the Author will be removed from the list items
        */
        public static void del_author(List<object> List_items){
            System.Console.Write("Enter Author Name To Delete:");
            string author=Console.ReadLine();
            List_items.RemoveAll(data=>data is Book && ((Book)data).Author==author);
            System.Console.WriteLine($"---All The Book's of {author} Author Is Deleted-----");
        }
        /*
        User will give a Book type Name {Novel,poem,literature etc}
        then all the Book's of the type will be shown from the list items
        */
        public static void del_book_by_type(List<object> List_items){
            System.Console.Write("Enter Book Type Name To Delete:");
            string type=Console.ReadLine();
            List_items.RemoveAll(data=>data is Book && ((Book)data).Type==type);
            System.Console.WriteLine($"---All The Book's of {type} Type Is Deleted-----");
        }
    }
    //CD class//
    public partial class CD:Media<string,int>{
        public string Artist;
        public CD(string artist,string title,string type,int date):base(title,type,date){
            this.Artist=artist;
        }
    }
    public partial class CD{
        /*
        To Delete or Update a particular item of CD from the list,first we store the index
        of the CD object's in cd_idx list.
        */
        static List<int> cd_idx=new List<int>();
        static int idx=0;
        public static void show_cd(List<object> List_items){
            int cnt=1;
            System.Console.WriteLine("\n----------------All The CD's Information---------------");
            foreach(var data in List_items){
                if(data is CD){
                    cd_idx.Add(idx);
                    CD it=(CD)data;
                    Console.WriteLine($"{cnt}.CD:\n---Title:{it.Title}\n---Author:{it.Artist}");
                    Console.WriteLine($"---CD Type:{it.Type}\n---Release Year:{it.Release_year}");
                    ++cnt;
                }
                ++idx;
            }
            System.Console.WriteLine("--------------------------------------------------------");
        }
        /*
        To delete a particular CD choosing from the CD list,first we take user 
        input  of the CD Serial number,then by using the index we store in the cd_idx
        list,we delete the partuar object from the list.
        */
        public static void delete_cd(List<object> List_items){
            int id,rem_idx;
            System.Console.Write("Enter the ID No of Book Want to remove:");
            id=int.Parse(Console.ReadLine());
            rem_idx=cd_idx[id-1];
            List_items.RemoveAt(rem_idx);
            System.Console.WriteLine($"------{id}.ID CD's Information Deleted-------");
        }
        public static void all_del_cd(List<object> List_items){
            List_items.RemoveAll(data=>data is CD);
            System.Console.WriteLine($"------All CD's Information Deleted-------");
        }
        /*
        To update a particular CD choosing from the CD list,first we take user 
        input  of the CD Serial number,then by using the index we store in the cd_idx
        list,we can update  the partuar object ,a particular information.
        */
        public static void cd_update(List<object> List_items){
            int id,up_idx,option;
            System.Console.Write("Enter the ID No of CD Want to Update:");
            id=int.Parse(Console.ReadLine());
            up_idx=cd_idx[id-1];
            System.Console.WriteLine("------------------------Menu--------------------------");
            System.Console.WriteLine("1.Update Artist Name\n2.Update Title");
            System.Console.WriteLine("3.Update Type\n4.Update Release Year");
            System.Console.Write("Enter Your Option:");
            option=int.Parse(Console.ReadLine());
            System.Console.WriteLine("------------------------------------------------------");
            if(option==1){
                string change;
                System.Console.Write("Enter Artist Name:");
                change=Console.ReadLine();
                ((CD)List_items[up_idx]).Artist=change;
            }
            else if(option==2){
                string change;
                System.Console.Write("Enter Title Name:");
                change=Console.ReadLine();
                ((CD)List_items[up_idx]).Title=change;
            }
            else if(option==3){
                string change;
                System.Console.Write("Enter Type:");
                change=Console.ReadLine();
                ((CD)List_items[up_idx]).Type=change;
            }
            else if(option==4){
                int change;
                System.Console.Write("Enter Release Year:");
                change=int.Parse(Console.ReadLine());
                ((CD)List_items[up_idx]).Release_year=change;
            }
            System.Console.WriteLine($"------{id}.ID CD's Information Updated-------");
        }
        public static void show_cd_by_recent_year(List<object> List_items){
            int cnt=1;
            var ans=from data in List_items where data is CD orderby ((CD)data).Release_year descending select data;
            System.Console.WriteLine("\n------------All The CD's Information-------------");
            foreach(var it in ans){
                Console.WriteLine($"{cnt}.CD:\n---Title:{((CD)it).Title}\n---Artist:{((CD)it).Artist}");
                Console.WriteLine($"---CD Type:{((CD)it).Type}\n---Release Year:{((CD)it).Release_year}");
                ++cnt;
            }
            System.Console.WriteLine("------------------------------------------------------");
        }
        /*
        User will give a CD artist Name {Habib,Jame's,Tahsan etc}
        then all the Book's of the type will be shown from the list items
        */
        public static void show_by_artist(List<object> List_items){
            System.Console.Write("Enter the Artist Name:");
            string artist=Console.ReadLine();
            int cnt=1;
            System.Console.WriteLine("\n-------------All The CD's Information-------------");
            var ans_artist=from data in List_items where (data is CD && ((CD)data).Artist==artist ) select data;
            foreach(var it in ans_artist){
                Console.WriteLine($"{cnt}.CD:\n---Title:{((CD)it).Title}\n---Author:{((CD)it).Artist}");
                Console.WriteLine($"---Book Type:{((CD)it).Type}\n---Release Year:{((CD)it).Release_year}");
                ++cnt;
            }
            System.Console.WriteLine("------------------------------------------------------");
        }
        /*
        User will give a CD Type Name {Bangla,Hindi,English etc}
        then all the CD's of the type will be shown from the list items
        */
        public static void show_by_cd_type(List<object> List_items){
            System.Console.Write("Enter the CD Type:");
            string type=Console.ReadLine();
            int cnt=1;
            var ans_type=from data in List_items where (data is CD && ((CD)data).Type==type ) select data;
            System.Console.WriteLine("\n--------------All The CD's Information------------");
            foreach(var it in ans_type){
                Console.WriteLine($"{cnt}.CD:\n---Title:{((CD)it).Title}\n---Author:{((CD)it).Artist}");
                Console.WriteLine($"---Book Type:{((CD)it).Type}\n---Release Year:{((CD)it).Release_year}");
                ++cnt;
            }
            System.Console.WriteLine("------------------------------------------------------");
        }
        /*
        User will give a CD Artist Name {Pitball,Habib,Minar etc}
        then all the CD's of the Artist will be removed from the list items
        */
        public static void del_artist(List<object> List_items){
            System.Console.Write("Enter CD Artist Name To Delete:");
            string artist=Console.ReadLine();
            List_items.RemoveAll(data=>data is CD && ((CD)data).Artist==artist);
            System.Console.WriteLine($"---All The CD's of {artist} Artist Information Deleted----");
        }
        /*
        User will give a CD Type Name {Bangla,hindi,Englis etc}
        then all the CD's of this Type will be removed from the list items
        */
        public static void del_cd_type(List<object> List_items){
            System.Console.Write("Enter CD Type To Delete:");
            string type=Console.ReadLine();
            List_items.RemoveAll(data=>data is CD && ((CD)data).Type==type);
            System.Console.WriteLine($"---All The CD's of {type} Type Information Deleted----");
        }
    }
    //DVD class//
    public partial class DVD:Media<string,int>{
        public string Director;
        public int box_office;
        public DVD(string director,string title,string type,int date,int box):base(title,type,date){
            this.Director=director;
            this.box_office=box;
        }
    }
    public partial class DVD{
        /*
        To Delete or Update a particular item of DVD from the list,first we store the index
        of the DVD object's in dvd_idx list.
        */
        static List<int> dvd_idx=new List<int>();
        static int idx=0;
        /*
        This method will show all the dvd object store in the list and also 
        store the object of the dvd type index in dvd_idx.
        */
        public static void show_dvd(List<object> List_items){
            int cnt=1;
            System.Console.WriteLine("----------------All the DVD information---------------");
            foreach(var data in List_items){
                if(data is DVD){
                    dvd_idx.Add(idx);
                    DVD it=(DVD)data;
                    Console.WriteLine($"{cnt}.DVD:\n---Title:{it.Title}\n---Director:{it.Director}");
                    Console.WriteLine($"---Book Type:{it.Type}\n---Release Year:{it.Release_year}\n---Box Office: ${it.box_office}M");
                    ++cnt;
                }
                ++idx;
            }
            System.Console.WriteLine("------------------------------------------------------");
        }
        /*
        To delete a particular dvd choosing from the dvd list,first we take user 
        input  of the dvd Serial number,then by using the index we store in the dvd_idx
        list,then we delete the partuar object from the list.
        */
        public static void delete_dvd(List<object> List_items){
            int id,rem_idx;
            System.Console.Write("Enter the ID No of Book Want to remove:");
            id=Convert.ToInt32(Console.ReadLine());
            rem_idx=dvd_idx[id-1];
            List_items.RemoveAt(rem_idx);
            System.Console.WriteLine($"-----{id} ID DVD information deleted-----");
        }
        public static void all_del_dvd(List<object> List_items){
            List_items.RemoveAll(data=>data is DVD);
            System.Console.WriteLine($"-----All the DVD information deleted-----");
        }
        /*
        To Update a particular dvd  information choosing from the dvd list,first we take user 
        input  of the dvd Serial number,then by using the index we store in the dvd_idx
        list,then we update  the particular object infromation of the particular dvd.
        */
        public static void dvd_update(List<object> List_items){
            int id,up_idx,option;
            System.Console.Write("Enter the ID No of DVD Want to remove:");
            id=int.Parse(Console.ReadLine());
            up_idx=dvd_idx[id-1];
            System.Console.WriteLine("-------------------------Menu-------------------------");
            System.Console.WriteLine("1.Update Director Name\n2.Update Title");
            System.Console.WriteLine("3.Update Type\n4.Update Release Year");
            System.Console.Write("Enter Option:");
            option=int.Parse(Console.ReadLine());
            System.Console.WriteLine("------------------------------------------------------");
            if(option==1){
                string change;
                System.Console.Write("Enter Director Name:");
                change=Console.ReadLine();
                ((DVD)List_items[up_idx]).Director=change;
            }
            else if(option==2){
                string change;
                System.Console.Write("Enter Title Name:");
                change=Console.ReadLine();
                ((DVD)List_items[up_idx]).Title=change;
            }
            else if(option==3){
                string change;
                System.Console.Write("Enter Type:");
                change=Console.ReadLine();
                ((DVD)List_items[up_idx]).Type=change;
            }
            else if(option==4){
                int change;
                System.Console.Write("Enter Release Year:");
                change=int.Parse(Console.ReadLine());
                ((DVD)List_items[up_idx]).Release_year=change;
            }
            System.Console.WriteLine($"-----{id} ID DVD information Updated-----");
        }
        public static void show_by_recent_year(List<object> List_items){
            int cnt=1;
            var ans=from data in List_items where data is DVD orderby ((DVD)data).Release_year descending select data;
            System.Console.WriteLine("--------All the DVD information of Recent Year-------");
            foreach(var it in ans){
                Console.WriteLine($"{cnt}.DVD:\n---Title:{((DVD)it).Title}\n---Director:{((DVD)it).Director}");     
                Console.WriteLine($"---DVD Type:{((DVD)it)     .Type}\n---Release Year:{((DVD)it).Release_year}\n---Box Office:${((DVD)it).box_office}M");     
                ++cnt;        
            }         
            System.Console.WriteLine("------------------------------------------------------");
        }  
        public static void show_by_box_office(List<object> List_items){
            int cnt=1;
            var ans=from data in List_items where data is DVD orderby ((DVD)data).box_office descending select data;
            System.Console.WriteLine("--------All the DVD information With Highest Box Office-------");
            foreach(var it in ans){
                Console.WriteLine($"{cnt}.DVD:\n---Title:{((DVD)it).Title}\n---Director:{((DVD)it).Director}");     
                Console.WriteLine($"---DVD Type:{((DVD)it).Type}\n---Release Year:{((DVD)it).Release_year}\n---Box Office:${((DVD)it).box_office}M");     
                ++cnt;        
            }         
            System.Console.WriteLine("---------------------------------------------------------------");
        }  
        /*          
        User will give a DVD Type Name {Action,Comedy etc}
        then all the  DVD  of the type will Show from the list items by LINQ                           
        */ 
        public static void show_by_dvd_type(List<object> List_items){                                                                            
            int cnt=1;
            /*
            first we store the answer then show the answer
            */
            System.Console.Write("Enter the DVD Type:");
            string type=Console.ReadLine();
            var ans_type=from data in List_items where (data is DVD && ((DVD)data).Type==type ) select data;
            System.Console.WriteLine($"-----All the DVD information of {type} Type DVD---------");
            foreach(var it in ans_type){
                Console.WriteLine($"{cnt}.DVD:\n---Title:{((DVD)it).Title}\n---Director:{((DVD)it).Director}");
                Console.WriteLine($"---DVD Type:{((DVD)it).Type}\n---Release Year:{((DVD)it).Release_year}\n---Box Office:{((DVD)it).box_office}");
                ++cnt;
            }
            System.Console.WriteLine("---------------------------------------------------------");
        }
        /*
        User will give a DVD Director Name {Cameron,Nolan etc}
        then all the  DVD of the Director will be removed from the list items
        */
        public static void show_by_director(List<object> List_items){
            int cnt=1;
            System.Console.Write("Enter DVD Director Name To Show:");
            string director=Console.ReadLine();
            var ans_director=from data in List_items where ( data is DVD && ((DVD)data).Director==director) select data;
            System.Console.WriteLine($"-----All the DVD information of {director} Director-------");
            foreach(var it in ans_director){
                Console.WriteLine($"{cnt}.DVD:\n---Title:{((DVD)it).Title}\n---Director:{((DVD)it).Director}");
                Console.WriteLine($"---DVD Type:{((DVD)it).Type}\n---Release Year:{((DVD)it).Release_year}\n---Box Office:{((DVD)it).box_office}");
                ++cnt;
            }
            System.Console.WriteLine($"----------------------------------------------------------");
        }
        /*
        User will give a dvd type {action,drama,comedy etc}
        then all these type of dvd will be removed from the list item
        */
        public static void del_dvd_type(List<object> List_items){
            System.Console.Write("Enter DVD Type Name To Delete:");
            string type=Console.ReadLine();
            List_items.RemoveAll(data=>data is DVD && ((DVD)data).Type==type);
            System.Console.WriteLine($"----All the DVD information of {type} Type Deleted----");
        }
        /*
        User will give a dvd Director name {Nolan,Cameron etc}
        then all these dvd of the director will be removed from the list item
        */
        public static void del_director(List<object> List_items){
            System.Console.Write("Enter DVD Director Name To Delete:");
            string director=Console.ReadLine();
            List_items.RemoveAll(data=>data is DVD && ((DVD)data).Director==director);
            System.Console.WriteLine($"----All the DVD information of {director} Director Deleted----");
        }
    }
    //----------------------Main Program---------------------//
    public class Main_Program{
        //-----this method will return the number of work a particular author/director/artist activities in the list----//
        public static void stat_(List<object> list){
            //finding the number of activity of a specific author/director/artist
            string check;
            System.Console.Write("Enter the Author/Director/Artist Name:");
            check=Console.ReadLine();
            var ans=from data in list 
            where ((data is Book && ((Book)data).Author==check) || ( data is CD && ((CD)data).Artist==check )|| (data is DVD && ((DVD)data).Director==check) ) select data;
            int cnt=ans.Count();
            System.Console.WriteLine($"The number of activity store in the list of {check} is:{cnt}");
        }
        public static void stat_info(List<object>list){
            System.Console.WriteLine("\n----------Statistics of Information Stored------------");
            var ans_b=from data in list where data is Book select data;
            var ans_c=from data in list where data is CD select data;
            var ans_d=from data in list where data is DVD select data;
            int book,cd,dvd,total;
            book=ans_b.Count();
            cd=ans_c.Count();
            dvd=ans_d.Count();
            total=book+cd+dvd;
            System.Console.WriteLine($"Total Media Information Stored:{total}");
            System.Console.WriteLine($"Number of Book's Information Stored:{book} and {(double)book/total*100:F2}%");
            System.Console.WriteLine($"Number of CD's Information Stored:{cd} and {(double)cd/total*100:F2}%");
            System.Console.WriteLine($"Number of DVD's Information Stored:{dvd} and {(double)dvd/total*100:F2}%");
            System.Console.WriteLine("-------------------------------------------------------");
        }
        //----------static Method To show all the Media Item stored------//
        static void show_all(List<object> list){
            System.Console.WriteLine("\n--------All the Media Item's Stored In The List--------");
            int cnt=1;
            foreach(var data in list){
                if(data is Book){
                    Book it=(Book)data;
                    Console.WriteLine($"{cnt}.Book:\n---Title:{it.Title}\n---Author:{it.Author}");
                    Console.WriteLine($"---Book Type:{it.Type}\n---Release Year:{it.Release_year}");
                }
                else if(data is CD){
                    CD it=(CD)data;
                    Console.WriteLine($"{cnt}.CD:\n---Title:{it.Title}\n---Artist:{it.Artist}");
                    Console.WriteLine($"---CD Type:{it.Type}\n---Release Year:{it.Release_year}");
                }
                else if(data is DVD){
                    DVD it=(DVD)data;
                    Console.WriteLine($"{cnt}.DVD:\n---Title:{it.Title}\n---Director:{it.Director}");
                    Console.WriteLine($"---DVD Type:{it.Type}\n---Release Year:{it.Release_year}\n---Box Office:${((DVD)it).box_office}M");
                }
                ++cnt;
            }
            System.Console.WriteLine("------------------------------------------------------");
        }
        //----------------------------Main function of the programme----------------------------//
        static void Main(string[] args){
            int n,op,nxt,nxtt,sho,option;
            List<object> List_items=new List<object>();
            //--------------Some Initialized Object of Different Book's Type------------//
            Book b1=new Book("Rabindranath","Gitanjali","Poetry",1929,"India Society",275);
            Book b2=new Book("Rabindranath","Kabuliwala","Short Story",1892,"Star Publication",248);
            Book b3=new Book("Rabindranath","Chitrangada","Novel",1892,"N/A",215);
            Book b4=new Book("Rabindranath","Gora","Novel",1910,"DM Library",230);
            Book b5=new Book("Kazi Nazrul Islam","Bidrohi","Poetry",1922,"Bijli",230);
            Book b6=new Book("Kazi Nazrul Islam","Dolonchapa","Ghazals",1923,"DM Library",170);
            Book b7=new Book("Kazi Nazrul Islam","Agnibina","Poetry",1922,"Arya",250);
            Book b8=new Book("Arif Azad","Paradoxical Sajid","Islamic",2018,"Somokalin",273);
            Book b9=new Book("Arif Azad","Bela Furabar Age","Islamic",2020,"Somokalin",473);
            //--------------Some Initialized Object of Different CD's Type------------//
            CD c1=new CD("James","Nagar Baul","Bangla",1993);
            CD c2=new CD("James","Poth Chola","Bangla",1996);
            CD c3=new CD("Habib","Shono","Bangla",2006);
            CD c4=new CD("Habib","Maya","Bangla",2017);
            CD c5=new CD("Jackson","Thriller","English",1982);
            CD c6=new CD("Shravan","Aashiqui","Hindi",1990);
            //--------------Some Initialized Object of Different DVD's Type------------//
            DVD d1=new DVD("Hirani","3 Idiots","Comedy",2009,90);
            DVD d2=new DVD("Cameron","titanic","Romance",1997,2257);
            DVD d3=new DVD("Darabont","The Shawshank Redemption","Crime",1994,733);
            DVD d4=new DVD("Nolan","Inception","Action",2010,839);
            DVD d5=new DVD("Lana","The Matrix","Action",1999,467);
            DVD d6=new DVD("Nolan","Interstellar","Sci-fi",2014,701);
            //------------------------------------------------------------------------//
            List_items.Add(b7);List_items.Add(c1);List_items.Add(b5);List_items.Add(d5);List_items.Add(c2);List_items.Add(b3);
            List_items.Add(c3);List_items.Add(d2);List_items.Add(b1);List_items.Add(d4);List_items.Add(b9);
            List_items.Add(d1);List_items.Add(b8);List_items.Add(c5);List_items.Add(c6);List_items.Add(d3);
            List_items.Add(b4);List_items.Add(c4);List_items.Add(b2);List_items.Add(b6);List_items.Add(d6);
            while(true){
                System.Console.WriteLine("\n--------------------------Menu------------------------");
                System.Console.WriteLine("1.Add Media Item's ");
                System.Console.WriteLine("2.Delete Media Item's ");
                System.Console.WriteLine("3.Update Media Item's ");
                System.Console.WriteLine("4.Show/Find Media Item's ");
                System.Console.WriteLine("5.Statistics of author/artist/director activities stored ");
                System.Console.WriteLine("6.Statistics of information Stored");
                System.Console.WriteLine("7.Exit");
                System.Console.Write("Enter Your Option:");
                n=int.Parse(Console.ReadLine());
                System.Console.WriteLine("------------------------------------------------------");
                if(n==1){
                    //add items//
                    System.Console.WriteLine("-------------------------Menu-------------------------");
                    System.Console.WriteLine("1.Add Book");
                    System.Console.WriteLine("2.Add CD ");
                    System.Console.WriteLine("3.Add DVD ");
                    System.Console.Write("Enter your Option:");
                    op=int.Parse(Console.ReadLine());
                    System.Console.WriteLine("------------------------------------------------------");
                    if(op==1){
                        //-----to add book----//
                        string author,title,type,publication;
                        int date,price;
                        System.Console.WriteLine("----------------Enter Book Information----------------");
                        System.Console.Write("---Book Auther Name:");
                        author=Console.ReadLine();
                        System.Console.Write("---Book Title Name:");
                        title=Console.ReadLine();
                        System.Console.Write("---Book Type Name:");
                        type=Console.ReadLine();
                        System.Console.Write("---Book Published Year:");
                        date=int.Parse(Console.ReadLine());
                        System.Console.Write("---Book Publication:");
                        publication=Console.ReadLine();
                        System.Console.Write("---Book Price:");
                        price=int.Parse(Console.ReadLine());
                        Book book=new Book(author,title,type,date,publication,price);
                        List_items.Add(book);
                        System.Console.WriteLine("------------------------------------------------------");
                    } 
                    else if(op==2){
                        //-----to add CD-----//
                        string artist,title,type;
                        int date;
                        System.Console.WriteLine("------------------Enter CD Information----------------");
                        System.Console.Write("---CD Artist Name:");
                        artist=Console.ReadLine();
                        System.Console.Write("---CD Title Name:");
                        title=Console.ReadLine();
                        System.Console.Write("---CD Type Name:");
                        type=Console.ReadLine();
                        System.Console.Write("---CD Published Year:");
                        date=int.Parse(Console.ReadLine());
                        CD cd=new CD(artist,title,type,date);
                        List_items.Add(cd);
                        System.Console.WriteLine("------------------------------------------------------");                      
                    }
                    else if(op==3){
                        //----to add DVD--------//
                        string director,title,type;
                        int date,box;
                        System.Console.WriteLine("-----------------Enter DVD Information---------------");
                        System.Console.Write("---DVD Director Name:");
                        director=Console.ReadLine();
                        System.Console.Write("---DVD Title Name:");
                        title=Console.ReadLine();
                        System.Console.Write("---DVD Type Name:");
                        type=Console.ReadLine();
                        System.Console.Write("---DVD Published Year:");
                        date=int.Parse(Console.ReadLine());
                        System.Console.Write("---DVD Box Office in Million:");
                        box=int.Parse(Console.ReadLine());
                        DVD dvd=new DVD(director,title,type,date,box);
                        List_items.Add(dvd);
                        System.Console.WriteLine("------------------------------------------------------");
                    }
                }
                else if(n==2){
                    //------------------delete items--------------------------//
                    System.Console.WriteLine("-------------------------Menu-------------------------");
                    System.Console.WriteLine("1.Delete From Book's ");
                    System.Console.WriteLine("2.Delete From CD's ");
                    System.Console.WriteLine("3.Delete From DVD's ");
                    System.Console.Write("Enter Option:");
                    nxt=int.Parse(Console.ReadLine());
                    System.Console.WriteLine("------------------------------------------------------");
                    if(nxt==1){
                        //-----------delete book items various format-----------//
                        System.Console.WriteLine("-----------------------Menu-------------------------");
                        System.Console.WriteLine("1.Delete By Manually");
                        System.Console.WriteLine("2.Delete By Author");
                        System.Console.WriteLine("3.Delete By Type");
                        System.Console.WriteLine("4.Delete all Book's");
                        System.Console.Write("Enter Option:");
                        option=int.Parse(Console.ReadLine());
                        System.Console.WriteLine("------------------------------------------------------");
                        switch(option){
                            case 1:{
                                Book.show_books(List_items);
                                Book.delete_book(List_items);
                                break;
                            }
                            case 2:{
                                Book.del_author(List_items);
                                break;
                            }
                            case 3:{
                                Book.del_book_by_type(List_items);
                                break;
                            }
                            case 4:{
                                Book.all_del_book(List_items);
                                break;
                            }
                        }
                    }
                    else if(nxt==2){
                        //--------------delete CD items various format-------------//
                        System.Console.WriteLine("------------------------Menu--------------------------");
                        System.Console.WriteLine("1.Delete By Manually");
                        System.Console.WriteLine("2.Delete By Artist");
                        System.Console.WriteLine("3.Delete By Type");
                        System.Console.WriteLine("4.Delete all CD's");
                        System.Console.Write("Enter Option:");
                        option=int.Parse(Console.ReadLine());
                        System.Console.WriteLine("------------------------------------------------------");
                        switch(option){
                            case 1:{
                                CD.show_cd(List_items);
                                CD.delete_cd(List_items);
                                break;
                            }
                            case 2:{
                                CD.del_artist(List_items);
                                break;
                            }
                            case 3:{
                                CD.del_cd_type(List_items);
                                break;
                            }
                            case 4:{
                                CD.all_del_cd(List_items);
                                break;
                            }
                        }
                    }
                    else if(nxt==3){
                        //-----------delete dvd item's various form-------------//
                        System.Console.WriteLine("------------------------Menu--------------------------");
                        System.Console.WriteLine("1.Delete By Manually");
                        System.Console.WriteLine("2.Delete By Director");
                        System.Console.WriteLine("3.Delete By Type");
                        System.Console.WriteLine("4.Delete all DVD");
                        System.Console.Write("Enter Option:");
                        option=int.Parse(Console.ReadLine());
                        System.Console.WriteLine("------------------------------------------------------");
                        switch(option){
                            case 1:{
                                DVD.show_dvd(List_items);
                                DVD.delete_dvd(List_items);
                                break;
                            }
                            case 2:{
                                DVD.del_director(List_items);
                                break;
                            }
                            case 3:{
                                DVD.del_dvd_type(List_items);
                                break;
                            }
                            case 4:{
                                DVD.all_del_dvd(List_items);
                                break;
                            }
                        }
                    }
                }
                else if(n==3){
                    //----------------update Media items------------------//
                    System.Console.WriteLine("-------------------------Menu-------------------------");
                    System.Console.WriteLine("1.Update From Book's ");
                    System.Console.WriteLine("2.Update From CD's ");
                    System.Console.WriteLine("3.Update From DVD's ");
                    System.Console.Write("Enter Your Option:");
                    nxtt=int.Parse(Console.ReadLine());
                    System.Console.WriteLine("------------------------------------------------------");
                    if(nxtt==1){
                        //-------------Update Book Detail's---------------//
                        Book.show_books(List_items);
                        Book.upadate_book(List_items);
                    }
                    else if(nxtt==2){
                        //-------------Update CD Detail's---------------//                       
                        CD.show_cd(List_items);
                        CD.cd_update(List_items);
                    }
                    else if(nxtt==3){
                        //-------------Update DVD Detail's---------------//
                        DVD.show_dvd(List_items);
                        DVD.dvd_update(List_items);
                    }
                }
                else if(n==4){
                    //-----------------show Media items-----------------//
                    System.Console.WriteLine("-------------------------Menu-------------------------");
                    System.Console.WriteLine("1.Show Book's Detail's ");
                    System.Console.WriteLine("2.Show CD's Detail's ");
                    System.Console.WriteLine("3.Show DVD's Detail's");
                    System.Console.WriteLine("4.Show all stored Media Detail's");
                    System.Console.Write("Enter Your Option:");
                    sho=int.Parse(Console.ReadLine());
                    System.Console.WriteLine("-------------------------------------------------------");
                    if(sho==1){
                        //------------show book items------------//
                        System.Console.WriteLine("------------------------Menu---------------------------");
                        System.Console.WriteLine("1.Show all Book's");
                        System.Console.WriteLine("2.Show/Find book's Recent");
                        System.Console.WriteLine("3.Show/Find Book's by auther");
                        System.Console.WriteLine("4.Show/Find Book's by type");
                        System.Console.WriteLine("5.Show/Find Book's by publication");
                        System.Console.WriteLine("6.Show/Find Book's by Lowest to Highest Price");
                        System.Console.WriteLine("7.Show/Find Book's by Highest to Lowest Price");
                        System.Console.Write("Enter Option:");
                        option=int.Parse(Console.ReadLine());
                        System.Console.WriteLine("--------------------------------------------------------");
                        switch(option){
                            case 1:{
                                Book.show_books(List_items);
                                break;
                            }
                            case 2:{
                                Book.show_book_recent_year(List_items);
                                break;
                            }
                            case 3:{
                                Book.show_by_author(List_items);
                                break;
                            }
                            case 4:{
                                Book.show_by_book_type(List_items);
                                break;
                            }
                            case 5:{
                                Book.show_publication(List_items);
                                break;
                            }
                            case 6:{
                                Book.show_low_high(List_items);
                                break;
                            }
                            case 7:{
                                Book.show_high_low(List_items);
                                break;
                            }
                        }
                    }
                    else if(sho==2){
                        //--------------show cd items----------------//
                        System.Console.WriteLine("------------------------Menu-------------------------");
                        System.Console.WriteLine("1.Show all CD's");
                        System.Console.WriteLine("2.Show/Find CD's Recent Year");
                        System.Console.WriteLine("3.Show/Find CD's by artist");
                        System.Console.WriteLine("4.Show/Find CD's by type");
                        System.Console.Write("Enter Option:");
                        option=int.Parse(Console.ReadLine());
                        System.Console.WriteLine("------------------------------------------------------");
                        switch(option){
                            case 1:{
                                CD.show_cd(List_items);
                                break;
                            }
                            case 2:{
                                CD.show_cd_by_recent_year(List_items);
                                break;
                            }
                            case 3:{
                                CD.show_by_artist(List_items);
                                break;
                            }
                            case 4:{
                                CD.show_by_cd_type(List_items);
                                break;
                            }
                        }
                    }
                    else if(sho==3){
                        //--------------show DVD items---------------//
                        System.Console.WriteLine("-------------------------Menu--------------------------");
                        System.Console.WriteLine("1.Show all DVD's");
                        System.Console.WriteLine("2.Show/Find DVD's in Recent Year");
                        System.Console.WriteLine("3.Show/Find DVD's by Director");
                        System.Console.WriteLine("4.Show/Find DVD's by type");
                        System.Console.WriteLine("5.Show/Find DVD's by Box Office Earning");
                        System.Console.Write("Enter Option:");
                        option=int.Parse(Console.ReadLine());
                        System.Console.WriteLine("------------------------------------------------------");
                        switch(option){
                            case 1:{
                                DVD.show_dvd(List_items);
                                break;
                            }
                            case 2:{
                                DVD.show_by_recent_year(List_items);
                                break;
                            }
                            case 3:{
                                DVD.show_by_director(List_items);
                                break;
                            }
                            case 4:{
                                DVD.show_by_dvd_type(List_items);
                                break;
                            }
                            case 5:{
                                DVD.show_by_box_office(List_items);
                                break;
                            }
                        }
                    }
                    else if(sho==4){
                        Main_Program.show_all(List_items);
                    }
                }
                else if(n==5) Main_Program.stat_(List_items);
                else if(n==6) Main_Program.stat_info(List_items);
                else if(n==7) break;
            }
        }
    }
    #pragma warning disable CS8600
    #pragma warning disable CS8601
    #pragma warning disable CS8604
}

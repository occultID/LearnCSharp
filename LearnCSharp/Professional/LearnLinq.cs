/*【206：LINQ】
 * LINQ概述
	• 语言集成查询（LINQ）是一系列直接将查询功能集成到C#语言的技术统称
		○ 数据查询历来都表示为简单的字符串，没有编译时类型检查或IntelliSense支持
		○ 针对每种类型的数据源，通常都需要了解不同的查询语言：SQL数据库、XML文档、各种Web服务等
		○ 借助LINQ，查询成为了最高级的语言构造，就像类、方法和事件一样
	• LINQ最明显的“语言集成”部分就是查询表达式
		○ 查询表达式采用声明性查询语法编写而成
		○ 使用查询语法，可以用最少的代码对数据源执行筛选、排序和分组操作
		○ 可使用相同的基本查询表达式模式来查询和转换SQL数据库、ADO.NET数据集、XML文档和流以及.NET集合中的数据
	• 查询表达式概述
		○ 查询表达式可用于查询并转换所有启用了LINQ的数据源中的数据
			§ 例如通过一个查询即可检索SQL数据库中的数据，并生成XML流作为输出
		○ 查询表达式易于掌握，因为它们使用了许多熟悉的C#语言构造
		○ 查询表达式中的变量全都是强类型，尽管在许多情况下，无需显示提供类型，因为编译器可以推断出
		○ 只有在循环访问查询变量后，才会执行查询
		○ 在编译时，查询表达式根据C#规范规则转换成标准查询运算符方法调用
			§ 可使用查询语法表示的任何查询都可以使用方法语法进行表示
				□ 大多数情况下，查询语法的可读性更高，也更为简洁
		○ 通常，建议在编写LINQ查询时尽量使用查询语法，并在必要时尽可能使用方法语法
			§ 两种语法在语义和性能上毫无差异
			§ 查询表达式通常比使用方法语法编写的等同表达式更具可读性
		○ 一些查询操作（如Count或Max）没有等效的查询表达式子句，因此必须表示为方法调用
			§ 可以使用各种方式结合使用方法语法和查询语法
		○ 查询表达式可被编译成表达式树或委托，具体视应用查询的类型而定
			§ IEnumerable<T>查询编译为委托
			§ IQueryable和IQueryable<T>查询编译为表达式树

LINQ查询基础
	• 查询是一种从数据源检索数据的表达式 
		○ 查询是一组指令，描述要从给定数据源（或源）检索的数据以及返回的数据应具有的形态和组织
			§ 查询与它生成的结果不同
	• 查询操作：所有LINQ查询操作都由以下三个不同的操作组成
		○ 获取数据源
			§ 数据源即提供数据的源，它可以是以下数据提供者
				□ SQL数据库
				□ XML文档
				□ JSON文档
				□ Web服务
				□ 字符串类型实例
				□ 集合实例
				□ 其他任何实现了IEnumerable<T>或IQueryable<T>接口的类或结构的对象
				□ 其他任意提供了LINQ查询支持的数据源
			§ 通常情况下数据源按逻辑方式组织为相同类型的元素的序列
				□ 对于该源序列，查询可能会执行三种操作之一
					® 检索元素的子集以生成新序列，而不修改各个元素
					® 检索元素的子集以生成新序列，并将序列转换为新类型的对象
					® 检索有关源数据的单独值，比如
						◊ 与特定条件匹配的元素数
						◊ 具有最大或最小值的元素
						◊ 与某个条件匹配的第一个元素，或指定元素集中特定值的总和
			§ 从应用程序的角度来看，原始数据源的特定类型和结构并不重要
				□ 应用程序始终将源数据视为IEnumerable<T>或IQueryable<T>集合
		○ 创建查询
			§ 查询指定要从数据源中检索的信息
				□ 查询可以指定在返回需求数据前对其进行排序、分组或结构化
				□ 使用查询对数据源进行检索并返回需求的数据
			§ 查询
				□ 查询操作简称为查询，包含下列三种语法
					® 查询语法
					® 方法语法
					® 查询语法和方法语法的组合查询
			§ 查询存储在查询变量中，并用查询表达式进行初始化
				□ 查询变量
					® 在LINQ中，查询变量是存储查询而不是查询结果的任何变量
					® 查询变量始终是可枚举类型
						◊ 在foreach语句或对其IEnumerator.MoveNext方法的直接调用中循环访问时会生成元素序列
						◊ 可以为查询遍历提供显示类型以便显示查询变量与select子句之间的类型关系
						◊ 也可以使用var关键字指示编译器在编译时推断查询变量的类型
					® LINQ查询中的类型关系
						◊ 不转换数据源的查询
							} 数据源的数据类型与查询结果的可枚举数据类型一致
						◊ 转换数据源的查询
							} 数据源的数据类型与查询结果的可枚举数据类型不一致
								– 查询的数据类型来源于数据源的某个数据元素
						◊ 让编译器推断类型信息
							} 使用var关键字声明查询变量，由编译器推断查询的类型
				□ 查询语法
					® 查询表达式是以查询语法表示的查询
					® 查询表达式时一流的语言结构
					® 查询表达式可以在其有效的任何上下文中使用
					® 查询表达式由一组类似于SQL或XQuery的声明性语法所编写的子句组成
						◊ 每个子句进而包含一个或多个C#表达式，而这些表达式可能本身是查询表达式或包含查询表达式
					® 查询表达式形式
						◊ 必须以from子句开头，且必须以select或group子句结尾
							} from子句：该子句指示数据源以及范围变量，该子句也称“开始查询表达式”
								– 形式
									w from 范围变量 in 数据源
								– 范围变量
									w 表示遍历源序列时，源序列中的每个连续元素
									w 基于数据源中元素的类型进行强类型化
									w 范围变量一直处于范围中，直到查询使用分号或continuation子句退出
								– 数据源
									w 用于提供数据的对象，该对象支持LINQ查询
								– 查询表达式可以包含多个from子句
									w 当数据源的元素是一个集合或包含集合时，可紧跟检索元素的from子句添加新的from子句
										> 该新from子句拥有新的范围变量
										> 该新from子句的数据源为上一子句范围变量或其包含的集合
							} select子句：该子句可生成所有其他类型的序列，该子句属于“结束查询表达式”
								– 形式
									w 其一：select 范围变量 --返回检索出的元素序列
									w 其二：select new { 匿名类属性与数据源元素的关联 } --返回包含检索结果的元素字段的生成的匿名类型
								– 范围变量（如使用）即from子句中的范围变量
								– 简单select子句只生成类型与数据源中包含的对象相同的对象序列
								– select子句也可应用于将源数据转换为新类型的序列
									w 此转换称为“投影”
							} group子句：该子句可生成按指定键组织的组的序列，该子句属于“结束查询表达式”
								– 形式
									w group 范围变量 by 键
								– 范围变量即from子句中的范围变量
								– 键
									w 可以是任何数据类型
										> 键可以是一个值或由数据元素来生成的包含多个值的匿名类
									w 范围变量中的元素根据指定的键来进行分组，并将包含键和元素的结果返回作为查询结果
						◊ 在第一个from子句与最后一个select或group子句之间，可以包含以下任意一个或多个子句
							} where子句：该子句可基于一个或多个谓词表达式，从数据源中筛选出元素
								– 形式
									w where 谓词表达式
								– 谓词表达式
									w 该表达式最终返回的是一个布尔值
										> 该表达式对需要进行筛选的数据源元素或其成员进行布尔判断
										> 该表达式可以是一个返回布尔值参数为筛选目标的方法
									w 可以使用&&或||连接多个谓词表达式
										> 当使用&&时相当于连续使用多个where子句进行组合筛选
							} orderby子句：该子句可按升序或降序对结果进行排序
								– 形式
									w orderby 键 ascending/descending
								– 键
									w 可以是任意支持排序的数据类型
										> 键可以是范围变量，或其成员
									w 可以使用多个键，通过逗号分隔每个键即可
									w 当多个键采取的排序方式不同时，可以连续使用多个orderby子句对不同键排序
								– ascending/descending
									w 指示排序采取升序（ascending）还是降序（descending）
									w 默认为ascending，采取默认排序时可省略该关键字
									w 使用降序时必须显示指定descending关键字 
							} join子句：该子句可基于每个元素中指定的键之间的相等比较，将一个数据源中的元素与另一个数据源中的元素进行关联或合并
								– 形式
									w 内部联接：join 外部范围变量 in 外部数据源 on 内部范围变量或其成员 equals 外部范围变量或其成员
									w 分组联接：join 外部范围变量 in 外部数据源 on 内部范围变量或其成员 equals 外部范围变量或其成员 into 临时变量
									w 左外部联接：join 外部范围变量 in 外部数据源 on 内部范围变量或其成员 equals 外部范围变量或其成员 into 临时变量
											   from 新范围变量 in 临时变量.DefaultIfEmpty()方法
							} let子句：该子句可将表达式（如方法调用）的结果存储在新范围变量中
								– 形式
									w let 新范围变量 = 操作原范围变量并返回新范围变量的表达式 
							} 嵌套的from子句
								– 查询子句本身可能包含查询表达式，其称为子查询
								– 每个子查询都以自己的from子句开头，该子句不一定指向第一个from子句的相同数据源
						◊ 可以使用into关键字将join子句、select子句或group子句的结果充当相同查询表达式中的其他查询子句的数据源
							} 形式
								– join、select或group子句 into 临时变量
				□ 方法语法
					® 标准查询运算符扩展方法
						◊ System.Linq命名空间中提供了一系列对IEnumerable<T>实例的扩展方法，这些方法用于实现LINQ查询
							} 查询语法都有对应的扩展方法与其关联
							} 扩展方法提供了更多的方法来实现查询
							} 可以在自定义IEnumerable<T>实现类中手动重新实现这些方法获取定制的或更优化的查询方案
						◊ 在编译时，查询表达式根据C#规范规则转换成标准查询运算符方法调用
							} 可使用查询语法表示的任何查询都可以使用方法语法进行表示
								– 大多数情况下，查询语法的可读性更高，也更为简洁
						◊ 通常，建议在编写LINQ查询时尽量使用查询语法，并在必要时尽可能使用方法语法
							} 两种语法在语义和性能上毫无差异
							} 查询表达式通常比使用方法语法编写的等同表达式更具可读性
						◊ 一些查询操作（如Count或Max）没有等效的查询表达式子句，因此必须表示为方法调用
						◊ 可以使用各种方式结合使用方法语法和查询语法
		○ 执行查询
			§ 延迟执行
				□ 查询变量本身只存储查询命令，查询的实际执行推迟到在foreach语句中循环访问查询变量之后进行
				□ foreach语句是检索查询结果的地方
				□ 由于查询变量本身从不保存查询结果，因此可以根据需要随意执行查询
			§ 强制立即执行
				□ 对一系列源元素执行聚合函数的查询必须首先循环访问这些元素
					® Count、Max、Average和First就属于此类查询
					® 由于查询本身必须使用foreach以便返回结果，因此这些查询在执行时不使用显示foreach语句
					® 这些类型的查询返回单个值，而不是IEnumerable集合
				□ 要强制立即执行任何查询并缓存其结果，可调用ToList、ToArray、ToDictionary或ToLookup方法
				□ 要强制立即执行任何查询可在查询表达式后紧跟一个foreach循环来实现
 */
using LearnCSharp.Basic;
using LearnCSharp.Professional.LearnCollectionsSpace;
using System.Numerics;

namespace LearnCSharp.Professional
{
    internal class LearnLinq
    {
		private static List<Author> authors = new List<Author>(20)
        {
            new Author(1,"周树人","鲁迅",'男'),
            new Author(2,"查良镛","金庸",'男'),
            new Author(3,"熊耀华","古龙",'男'),
            new Author(4,"温瑞安","温瑞安",'男'),
            new Author(5,"陈文统","梁羽生",'男'),
            new Author(6,"徐  磊","南派三叔",'男'),
            new Author(7,"张  威","唐家三少",'男'),
            new Author(8,"张牧野","天下霸唱",'男'),
            new Author(9,"李  虎","天蚕土豆",'男'),
            new Author(10,"陈  喆","琼瑶",'女'),
            new Author(11,"张爱玲","梁京",'女'),
            new Author(12,"杨季康","杨绛",'女'),
            new Author(13,"钱钟书","钱钟书",'男'),
            new Author(14,"唐  七","唐七",'女'),
            new Author(15,"周浩晖","周浩晖",'男'),
            new Author(16,"张  恒","zhttty",'男'),
            new Author(17,"谢婉莹","冰心",'女'),
            new Author(18,"李尧棠","巴金",'男'),
            new Author(19,"吴  鑫","卖报小郎君",'男'),
            new Author(20,"陈政华","烽火戏诸侯",'男')
        };

		private static List<Book> books = new List<Book>(20)
		{
			new Book("鬼 吹 灯",authors[7].AuthorID),
			new Book("迷踪之国",authors[7].AuthorID),
			new Book("死亡循环",authors[7].AuthorID),
			new Book("盗墓笔记",authors[5].AuthorID),
			new Book("老 九 门",authors[5].AuthorID),
			new Book("斗罗大陆",authors[6].AuthorID),
			new Book("神印王座",authors[6].AuthorID),
			new Book("射雕英雄", authors[1].AuthorID),
			new Book("神雕侠侣", authors[1].AuthorID),
			new Book("天龙八部", authors[1].AuthorID),
			new Book("小李飞刀", authors[2].AuthorID),
			new Book("圆月弯刀", authors[2].AuthorID),
			new Book("楚 留 香", authors[2].AuthorID),
			new Book("斗破苍穹", authors[8].AuthorID),
			new Book("茉莉香片", authors[10].AuthorID),
			new Book("倾城之恋", authors[10].AuthorID),
			new Book("华 胥 引", authors[13].AuthorID),
			new Book("烟雨蒙蒙", authors[9].AuthorID),
			new Book("剑    来", authors[19].AuthorID),
			new Book("白发魔女", authors[4].AuthorID)
		};

		private static List<Score> scores;

        /*【20601：LINQ查询语句】*/
        public static void LearnLINQQuerySyntax()
        {
            Console.WriteLine("\n------示例：Linq查询语法------\n");
            ShowList();

            //使用Linq查询语法输出按作者信息分组的书籍信息
            Console.WriteLine("按作者信息分组查询书籍信息");
			var booksGroupByAuthor = from book in books
									 from author in authors
									 where book.AuthorID == author.AuthorID
									 group book by author;
			foreach (var books in booksGroupByAuthor)
			{
                Console.WriteLine(books.Key.Pseudonym);
				foreach (var book in books)
				{
                    Console.WriteLine($"    {book}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("使用并列from子句联接books、authors和scores集合并导出新集合并按评分分组和按从高到低输出书籍评分信息：");
			var bookScores = from book in books
							 from author in authors
							 from score in scores
							 where book.Guid == score.Guid && book.AuthorID == author.AuthorID
							 select new { BookName = book.BookName, Author = author.Pseudonym, Score = score.BookScore }
							 into bookScoresInfo
							 orderby bookScoresInfo.Score descending
							 group bookScoresInfo by bookScoresInfo.Score >= 8.0 ? "高于8分" : "低于8分";

			foreach (var booksInfo in bookScores)
			{
				Console.WriteLine(booksInfo.Key);
				foreach (var book in booksInfo)
				{
                    Console.WriteLine($"    书籍评分信息 --- 书籍：{book.BookName} | 评分：{book.Score:0.00} | 作者：{book.Author}");
                }
				Console.WriteLine();
			}

			Console.WriteLine("使用join子句联接books、authors集合并按作家性别、笔名对书籍信息进行分组再进行输出：");
			var booksGroupByAuthorAndAuthorGender = from book in books
													join author in authors
													on book.AuthorID equals author.AuthorID
													let booksInfo = new { BookName = book.BookName, Author = author.Pseudonym, Gender = author.Gender == '男' ? "男作家" : "女作家" }
													group booksInfo by booksInfo.Gender
													into booksInfoByGender
													select new
													{
														Gender = booksInfoByGender.Key,
														Books = from book in booksInfoByGender
																  group book by book.Author
													};

			foreach (var booksInfo in booksGroupByAuthorAndAuthorGender) 
			{
				Console.WriteLine(booksInfo.Gender);
				foreach (var books in booksInfo.Books)
				{
					Console.WriteLine($"    {books.Key}");
					foreach (var book in books)
					{
						Console.WriteLine($"        {book.BookName}");
					}
					Console.WriteLine();
				}
				Console.WriteLine();
			}

			Console.WriteLine("使用join子句对authors、books和scores进行联接");
			var booksFullInfo = from author in authors
								join book in books on author.AuthorID equals book.AuthorID
								into books
								from bookInfo in books.DefaultIfEmpty(new Book("未 收 录", -1))
								let book = new { BookName = bookInfo.BookName, Author = author.Pseudonym, Guid = bookInfo.Guid }
								join score in scores on book.Guid equals score.Guid
								into bookinfo 
								from bi in bookinfo.DefaultIfEmpty(new Score(new Guid(),0.0))
								let bifon = new { BookName = book.BookName, Author = book.Author, Score = bi.BookScore }
                                group bifon by bifon.Author;

			foreach (var item in booksFullInfo)
			{
				Console.WriteLine(item.Key);
				foreach (var book in item)
				{
                    Console.WriteLine($"    书籍信息 --- 书籍：{book.BookName} | 评分：{book.Score:0.00} | 作者：{book.Author}");
                }
			}
        }

		/*【20602：LINQ方法】*/
		public static void LearnLINQMethodSyntax()
		{
            Console.WriteLine("\n------示例：Linq方法------\n");
            ShowList();
		}

		private static void ShowList()
		{
            Console.WriteLine("使用List<T>类创建了一个Author类的列表实例authors");
            Console.WriteLine();
            Console.WriteLine("向列表authors中随机添加了20个author类实例，并遍历列表内所有数据：");

            foreach (Author author in authors)
            {
                Console.WriteLine(author);
            }

            Console.WriteLine();

            Console.WriteLine("使用List<T>类创建了一个Book类的列表实例books");
            Console.WriteLine();
            Console.WriteLine("向列表books中添加了20个Book类实例，并遍历列表内所有数据：");

            foreach (Book book in books)
            {
                Console.WriteLine(book);
            }
            Console.WriteLine();

            Console.WriteLine("使用List<T>类创建了一个Score类的列表实例scores");
            Console.WriteLine();
            Console.WriteLine("向列表scores中添加了20个Score类实例，并遍历列表内所有数据：");

			scores = new List<Score>(20);

			for (int i = 0; i < 20; i++)
			{
				scores.Add(new Score(books[i].Guid, Random.Shared.Next(5, 10) + Random.Shared.NextDouble()));
			}

            foreach (Score score in scores)
            {
                Console.WriteLine(score);
            }
            Console.WriteLine();
        }

		public static void StartLearnLinq()
		{
            Console.WriteLine("【学习LINQ代码运行示例】");

            string title = "001 LINQ查询语法\n" +
                "002 LINQ方法语法";

            do
            {
                Console.WriteLine("【LINQ学习代码示例演示】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001": LearnLINQQuerySyntax(); break;
                    case "002": LearnLINQMethodSyntax(); break;
                    default: Console.WriteLine("输入错误！"); break;
                }

                Console.WriteLine();
                Console.WriteLine("是否继续查询和运行本章节其他代码：直接按下Enter继续，否则即退出");

                if (Console.ReadKey(true).Key != ConsoleKey.Enter)
                    break;
                Console.WriteLine("\n");
            }
            while (true);
        }

        private class Book : IEquatable<Book>
        {
            public Guid Guid { get; init; }
            public string BookName
            {
                get;
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                        field = value;
                    else
                        throw new ArgumentException(nameof(BookName));
                }
            }
            public int AuthorID
            {
                get;
				set;
            }

            public Book(string bookName, int authorID)
            {
                Guid = Guid.NewGuid();
                BookName = bookName;
                AuthorID = authorID;
            }

            public bool Equals(Book? other)
            {
				if (other is null) return false;

				if (other.GetHashCode() == GetHashCode())
					return true;

                bool isSameGuid = this.Guid == other?.Guid;
                bool isSameName = this.BookName == other?.BookName;
                bool isSameAuthor = this.AuthorID == other?.AuthorID;

                if (isSameGuid) return true;
                if (isSameName && isSameAuthor) return true;

                return false;
            }

            public static bool operator ==(Book left, Book right)
            {
                if (left is null) return false;
                return left.Equals(right);
            }

            public static bool operator !=(Book left, Book right)
            {
                if (left is null) return false;
                return !left.Equals(right);
            }

            public override bool Equals(object? obj)
            {
                return obj is Book book ? Equals(book) : false;
            }

            public override string ToString()
            {
                return $"书籍信息 --- 书籍ID：{Guid} | 书名：《{BookName}》";
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        private class Author:IEquatable<Author>
        {
            public int AuthorID { get; private set; }
            public string AuthorName { get; private set; }

			public string Pseudonym { get; private set; }

			public char Gender { get; private set; }

            public Author(int authorID, string authorName, string pseudonym, char gender)
            {
				AuthorID = authorID;
				AuthorName = authorName;
				Pseudonym = pseudonym;
				Gender = gender;
            }

            public override string ToString()
            {
				return $"作者信息 --- ID：{AuthorID,3} | 姓名：{AuthorName} | 笔名：{Pseudonym}";
            }

            public bool Equals(Author? other)
            {
                if (other is null) return false;

				if (other.GetHashCode() == GetHashCode())
					return true;

				if (other.AuthorName == AuthorName && other.Pseudonym == Pseudonym)
					return true;

				return false;
            }

            public override bool Equals(object? obj)
            {
				if (obj is Author author)
					return Equals(author);

				return false;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public static bool operator ==(Author left, Author right)
			{
				if (left is null) return false;

				return left.Equals(right);
			}

			public static bool operator !=(Author left, Author right)
			{
				if (left is null) return false;

				return !left.Equals(right);
			}
        }

		private record Score
		{
			public Guid Guid { get; init; }
			public double BookScore { get; init; }

			public Score(Guid guid, double score)
			{
				Guid = guid;
				BookScore = score;
			}

            public void Deconstruct(out Guid guid, out double score)
            {
				(guid, score) = (Guid, BookScore);
            }

            public override string ToString()
            {
				return $"书籍评分 --- 书籍ID：{Guid} | 书籍评分：{BookScore:0.00}";
            }
        }
    }
}

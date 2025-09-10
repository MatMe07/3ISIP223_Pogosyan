Console.Write("Ввeдите количество операций (от 2 до 40): ");
int n = Convert.ToInt32( Console.ReadLine());

string[] tovars = new string[n];
double[] prices = new double[n];


Console.WriteLine("Ввeдите (Название услуги или товара; Количество денег): ");
for (int i = 0;i < n; i++)
{
    Console.Write($"\n{i+1} товар: ");
    string[] tov = Console.ReadLine().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
    tovars[i] = tov[0];
    prices[i] = Convert.ToInt32(tov[1]);
}

double maxx = prices[0], minn = prices[0], summ = 0;
double meann;

void vivod()
{
    Console.WriteLine("---------------------------");
    for (int i = 0; i < n; i++)
    {
        Console.WriteLine($"({tovars[i]}; {prices[i]})");
    }
    Console.WriteLine("---------------------------");
}

void stat()
{
    for (int i = 0; i < n; i++)
    {
        double numb = prices[i];
        summ += numb;
        if (numb > maxx) maxx = numb;
        if (numb < minn) minn = numb;

    }
    meann = summ / prices.Length;
    Console.WriteLine($"mean = {meann}, max = {maxx}, min = {minn}, sum = {summ}");
}

void sorts()
{
    Console.WriteLine("Сортировка по цене");
    for (int i = 0; i < n; i++)
    {
        for (int j = i + 1; j < n; j++)
        {
            if (prices[i] > prices[j])
            {
                double tmep = prices[i];
                prices[i] = prices[j];
                prices[j] = tmep;

                string tmp = tovars[i];
                tovars[i] = tovars[j];
                tovars[j] = tmp;
            }
        }
    }
    vivod();
}
void convertsValut(double n)
{
    for (int i = 0; i < prices.Length; i++)
    {
        prices[i] = prices[i]/ n;
    }
}

void Poisk(string txt)
{
    for (int i = 0; i < n; i++) { 
        if (txt == tovars[i])
        {
            Console.WriteLine($"({tovars[i]}; {prices[i]})");
            return;
        }
    }
    Console.WriteLine("Нету такого товара!");
}

int vhod;
do
{
    //Console.Clear();
    Console.Write("1. Вывод данных\n2. Статистика\n3. Сортировка по цене\n4. Конвертация валюты\n5. Поиск по названию\n0. Выход\nВведите цифру: ");
    vhod = Convert.ToInt32(Console.ReadLine());
    switch (vhod)
    {
        case 0: break;
        case 1:
            {
                vivod();
                break;
            }
        case 2:
            {
                stat();
                break;
            }
        case 3:
            {
                sorts();
                break;
            }
        case 4:
            {
                Console.Write("Введите валюту для конвертации (d - доллар, u - юань, y - йен): ");
                char valut = Convert.ToChar(Console.ReadLine());
                if (valut == 'd') convertsValut(83.24);
                else if (valut == 'u') convertsValut(11.69);
                else if (valut == 'y') convertsValut(0.565);
                else Console.WriteLine("Error!!!!!!!!!!!!!!");
                break;
            }
        case 5:
            {
                Console.Write("Введите название для поиска: ");
                string tovar = Console.ReadLine();
                Poisk(tovar);
                break;
            }
    }
    //Console.ReadLine();
}
while(vhod != 0);
Console.WriteLine();


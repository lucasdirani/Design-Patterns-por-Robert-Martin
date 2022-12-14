# Design Patterns por Robert Martin

Repositório com o objetivo de compartilhar exemplos de design patterns elaborados por Robert C. Martin em seu livro *Princípios, Padrões e Práticas Ágeis em C#*.

Os textos a respeito de cada padrão de projeto foram publicados originalmente no meu blog no Medium, [**Fora de Assunto**](https://medium.com/fora-de-assunto).

# Saiba mais sobre os design patterns

- [Command](#command)
  - [Command e a simplicidade](#command-e-a-simplicidade)
  - [Utilizando Command na vida real](#utilizando-command-na-vida-real)
  - [Command e transações](#command-e-transações)
  - [O método Undo](#o-método-undo)
  - [1001 utilidades](#1001-utilidades)
- [Template Method e Strategy](#template-method-e-strategy)
  - [Template Method](#template-method)
  - [Strategy](#strategy)
  - [Template Method ou Strategy?](#template-method-ou-strategy)
- [Façade e Mediator](#façade)
  - [Façade](#façade)
  - [Mediator](#mediator)
  - [Escolhendo entre Façade e Mediator](#escolhendo-entre-façade-e-mediator)
- [Singleton e Monostate](#singleton-e-monostate)
  - [Singleton](#singleton)
  - [Monostate](#monostate)
  - [Escolhendo entre Singleton e Monostate](#escolhendo-entre-singleton-e-monostate)
- [Null Object](#null-object)
  - [Implementando o padrão](#implementando-o-padrão)
  - [O poder de Null Object](#o-poder-de-null-object)

# Command

O mundo da programação orientada a objetos está repleto de padrões de projeto, também conhecidos como design patterns. Muitos deles foram elaborados e condensados pela Gang Of Four, que trouxe uma gigantesca contribuição para a comunidade de desenvolvimento de software.

O pattern Command figura na lista dos padrões apresentados originalmente por Erich Gamma, Richard Helm, Ralph Johnson e John Vlissides. Porém, aqui a introdução do padrão será feita sob a óptica do ilustríssimo Uncle Bob.

## Command e a simplicidade

Na sua obra Princípios, Padrões e Práticas Ágeis em C#, Robert Martin coloca o padrão de projeto Command na lista dos mais simples e elegantes. Entretanto, o autor faz um aviso importante: ainda que seja um pattern descomplicado, Command possui uma aplicabilidade praticamente infinita, característica que o torna útil para diversas ocasiões.

Conforme podemos visualizar logo a seguir, Command é composto apenas por uma interface contendo o método void Execute. Assim sendo, entende-se a razão de Uncle Bob descrevê-lo como um padrão simples.

``` csharp
public interface ICommand
{
    void Execute();
}
```

Martin destaca que diferente da maioria das classes que associam um conjunto de métodos a um conjunto de variáveis, Command acaba seguindo uma rota totalmente oposta: o padrão encapsula um método que não interage com variáveis na sua assinatura, trazendo algumas particularidades do paradigma funcional ao universo da orientação a objetos.

## Utilizando Command na vida real

Ainda em seu livro, Robert conta que há muitos anos prestou consultoria a uma empresa que fabricava fotocopiadoras. Ele tinha a missão de auxiliar a equipe de programadores a estruturar um software embarcado que controlava o funcionamento interno de uma nova copiadora.

Durante o período de desenvolvimento, Uncle Bob e os demais membros do projeto tiveram a ideia de implementar o padrão Command para resolver uma grande parte dos desafios impostos pelo contexto do programa. O design da solução está retratado no diagrama abaixo, retirado do próprio capítulo escrito por Bob.

<p align="center">
<img width="600" src="https://miro.medium.com/max/828/1*MMS0RmEDVNNJsQH3QwhIWA.jpeg" alt="Diagrama de classes do projeto da copiadora">
</p>

Disponível em todas as classes expostas no diagrama, o método Execute obteve uma responsabilidade em cada comando. Ao chamar a função em RelayOnCommand, um relé era ativado na máquina. A mesma ação em MotorOffCommand desligava o seu motor, e assim por diante. A identificação dos componentes da copiadora, como o relé ou o motor, era passada nos parâmetros do construtor das classes Command.

Além do pattern Command, o sistema funcionava através de eventos, que por sua vez eram captados por sensores. Para deixar essa arquitetura mais compreensível, Robert explica em seu livro um cenário concreto da aplicação:

[![Readme Quotes](https://quotes-github-readme.vercel.app/api?type=horizontal&theme=dark&quote=Quando%20um%20sensor%20%C3%B3ptico%20determinava%20que%20uma%20folha%20de%20papel%20tinha%20atingido%20certo%20ponto%20na%20trajet%C3%B3ria%20do%20papel%2C%20precis%C3%A1vamos%20acionar%20certo%20engate.%20Pudemos%20implementar%20isso%20simplesmente%20vinculando%20o%20ClutchOnCommand%20apropriado%20ao%20objeto%20que%20controlava%20o%20sensor.&author=Robert%20C.%20Martin)](https://github.com/piyushsuthar/github-readme-quotes)

A partir deste relacionamento entre as classes que representavam os sensores e a interface Command, o objeto do tipo Sensor ficava desinformado sobre o seu comportamento. Isso porque um sensor não conhecia qual implementação de Command estava sendo utilizada em tempo de compilação, simplificando portanto todo o seu funcionamento.

Martin resume a beleza do padrão ao comentar a respeito do desacoplamento obtido com o seu uso:

[![Readme Quotes](https://quotes-github-readme.vercel.app/api?type=horizontal&theme=dark&quote=Em%20algum%20ponto%20durante%20a%20inicializa%C3%A7%C3%A3o%20do%20sistema%2C%20cada%20objeto%20Sensor%20%C3%A9%20vinculado%20a%20um%20objeto%20Command%20apropriado.%20Isso%20coloca%20todas%20as%20interliga%C3%A7%C3%B5es%20l%C3%B3gicas%20entre%20os%20sensores%20e%20comandos%20em%20um%20%C3%BAnico%20lugar%2C%20e%20as%20retira%20do%20corpo%20principal%20do%20programa.%0A&author=Robert%20C.%20Martin)](https://github.com/piyushsuthar/github-readme-quotes)

## Command e transações

Uncle Bob especifica ainda outro papel recorrente desempenhado pelo pattern — a criação e execução de transações. De modo a ilustrar essa interessante finalidade, Robert conjectura um software que administra um banco de dados de funcionários. Nele, os usuários estão aptos a adicionar, excluir ou alterar os dados desses empregados.

A fim de incluir um novo funcionário, o sistema deve necessariamente verificar se as informações cadastrais estão corretas antes de persistir o registro na base. É nesse ponto que o padrão Command pode ser inserido, segundo o autor:

[![Readme Quotes](https://quotes-github-readme.vercel.app/api?type=horizontal&theme=dark&quote=O%20objeto%20comando%20atua%20como%20um%20reposit%C3%B3rio%20dos%20dados%20n%C3%A3o%20validados%2C%20implementa%20os%20m%C3%A9todos%20de%20valida%C3%A7%C3%A3o%20e%20implementa%20os%20m%C3%A9todos%20que%20finalmente%20executam%20a%20transa%C3%A7%C3%A3o.%0A&author=Robert%20C.%20Martin)](https://github.com/piyushsuthar/github-readme-quotes)

<p align="center">
<img width="600" src="https://miro.medium.com/max/828/1*msvA71yXxMHvIdxpqh0Aag.jpeg" alt="Diagrama de classes do exemplo do banco de dados de funcionários">
</p>

No diagrama de classes elaborado para o exemplo, a interface Transaction corresponde a interface Command. O método Validate assegura que os dados enviados pelo usuário estão aderentes às regras impostas pelo sistema, podendo até realizar uma verificação com o intuito de garantir a coerência entre o estado atual do objeto e o banco de dados. Ou seja, se certificar que o funcionário ainda não foi cadastrado na base.

Isto posto, um novo objeto do tipo Employee é criado com os campos do objeto AddEmployeeTransaction, ao passo que PayClassification também acaba sendo copiado para Employee. Com a instanciação do objeto que representa o funcionário, a função Execute consome os dados validados para atualizar o banco de dados.

Para encerrar, Bob ressalta um dos principais benefícios conquistados pelo encapsulamento de transações com o padrão Command:

[![Readme Quotes](https://quotes-github-readme.vercel.app/api?type=horizontal&theme=dark&quote=A%20vantagem%20que%20isso%20nos%20proporciona%20%C3%A9%20o%20excepcional%20desacoplamento%20do%20c%C3%B3digo%20que%20obt%C3%A9m%20os%20dados%20do%20usu%C3%A1rio%2C%20do%20c%C3%B3digo%20que%20valida%20e%20opera%20nesses%20dados%20e%20dos%20pr%C3%B3prios%20objetos%20de%20neg%C3%B3cio.%20%0A&author=Robert%20C.%20Martin)](https://github.com/piyushsuthar/github-readme-quotes)

## O método Undo

De acordo com o que foi detalhado na seção anterior, Command pode possuir uma variação transacional em sua implementação. Todavia, há ainda outra possibilidade oferecida pelo padrão, que pode ser extremamente eficiente e vantajosa em inúmeras ocasiões: a funcionalidade Undo (Desfazer).

``` csharp
public interface ICommand
{
    void Execute();
    void Undo();
}
```

Com o objetivo de entender como este método se encaixa no design pattern, irei recorrer a mais um exemplo criado por Robert Martin. Suponha que um grupo de usuários acesse um aplicativo capaz de desenhar formas geométricas na tela. Uma das ações requisitadas frequentemente no sistema é o desenho de círculos.

Imaginando o hipotético código do programa, a solicitação para imprimir um círculo no monitor cria um objeto DrawCircleCommand, que possui o método Execute. Quando o usuário confirma com o mouse que deseja inserir o círculo na tela, o objeto inclui esta forma geométrica na lista de desenhos exibidos e armazena o seu identificador único de modo privado. Na sequência, o sistema coloca DrawCircleCommand na pilha dos comandos concluídos.

Caso o usuário não tivesse gostado do círculo desenhado e quisesse removê-lo, ele clicaria na operação de desfazer na barra de ferramentas. O aplicativo então leria todos os comandos armazenados na pilha de concluídos, e por consequência chamaria o método Undo no objeto Command procurado. Por fim, a figura geométrica seria apagada da tela, simples assim!

[![Readme Quotes](https://quotes-github-readme.vercel.app/api?type=horizontal&theme=dark&quote=Com%20essa%20t%C3%A9cnica%2C%20voc%C3%AA%20pode%20implementar%20Undo%20de%20maneira%20f%C3%A1cil%20em%20praticamente%20qualquer%20aplicativo.%20O%20c%C3%B3digo%20que%20sabe%20como%20desfazer%20um%20comando%20est%C3%A1%20sempre%20pr%C3%B3ximo%20ao%20c%C3%B3digo%20que%20sabe%20como%20executar%20o%20comando.&author=Robert%20C.%20Martin)](https://github.com/piyushsuthar/github-readme-quotes)

## 1001 utilidades

Acredito que tenha sido possível notar que o padrão de projeto Command se adequa muito bem em múltiplas situações. Sem dúvida alguma essa é a maior beleza do pattern, que demonstra ser flexível e simples de implementar.

[(Voltar para o topo)](#saiba-mais-sobre-os-design-patterns)

# Template Method e Strategy

Segundo os relatos de Martin, durante a década de 1990, quando a orientação a objetos estava dando os seus primeiros passos, o conceito de herança se popularizou entre os programadores. Isso porque este pilar da POO permitia que os desenvolvedores reaproveitassem boa parte do seu código, modificando apenas operações específicas nas subclasses.

Entretanto, não demorou muito tempo para que algumas pessoas notassem os impactos de se utilizar a herança de modo desgovernado. Uncle Bob conta que foi a partir de 1995, na época em que Gamma, Helm, Johnson e Vlissides lançaram o seu famoso livro de design patterns, que os efeitos colaterais da herança foram expostos de fato. Inclusive, os autores destacaram o seguinte:

[![Readme Quotes](https://quotes-github-readme.vercel.app/api?type=horizontal&theme=dark&quote=Favore%C3%A7a%20a%20composi%C3%A7%C3%A3o%20de%20objetos%20%28delega%C3%A7%C3%A3o%29%20em%20detrimento%20da%20heran%C3%A7a%20de%20classe.&author=Robert%20C.%20Martin)](https://github.com/piyushsuthar/github-readme-quotes)

A dicotomia entre herança e delegação pode ser bem retratada pelos padrões de projeto Template Method e Strategy. Os dois patterns resolvem o mesmo problema, que basicamente se resume a separar um algoritmo genérico de um contexto detalhado.

## Template Method

Com o objetivo de assimilarmos o funcionamento do padrão Template Method, mostrarei a seguir vários trechos de código elaborados por Robert Martin em sua obra.

Para os programadores de C#, gostaria apenas de fazer uma observação. Os exemplos foram feitos em uma época que ainda não tínhamos o recurso de Generics na linguagem, e por conta disto a solução exigiu uma abordagem diferente.

Sem mais delongas, considerem uma classe responsável por ordenar um array de inteiros usando o algoritmo Bubble Sort.

``` csharp
public class BubbleSorter
{
    static int operations = 0;
    public static int Sort(int[] array)
    {
        operations = 0;
        if (array.Length <= 1)
            return operations;
        for (int nextToLast = array.Length - 2; nextToLast >= 0; nextToLast--)
        {
            for (int index = 0; index <= nextToLast; index++)
            {
                CompareAndSwap(array, index);
            }
        }
        return operations;
    }
    private static void CompareAndSwap(int[] array, int index)
    {
        if (array[index] > array[index+1])
        {
            Swap(array, index);
        }
        operations++;
    }
    private static void Swap(int[] array, int index)
    {
        int temp = array[index];
        array[index] = array[index + 1];
        array[index + 1] = temp;
    }
}
```

Reparem que o método Sort encapsula a lógica de ordenação. Ele consome os outros dois métodos da classe, Swap e CompareAndSwap, que manipulam o array e se responsabilizam pelos detalhes do algoritmo.

A estrutura de BubbleSorter pode ser melhorada com a implementação do pattern Template Method. Aplicando essa refatoração, a classe não terá mais a preocupação de saber qual é o tipo do vetor sendo ordenado, e irá transferir ainda a responsabilidade de comparar e trocar elementos fora de ordem para as suas subclasses.

``` csharp
public abstract class BubbleSorter
{
    private int operations = 0;
    protected int length = 0;
    protected int DoSort()
    {
        operations = 0;
        if(length <= 1)
        {
            return operations;
        }
        for (int nextToLast = length - 2; nextToLast >= 0; nextToLast--)
        {
            for (int index = 0; index <= nextToLast; index++)
            {
                if (OutOfOrder(index))
                {
                    Swap(index);
                }
                operations++;
            }
        }
        return operations;
    }
    protected abstract void Swap(int index);
    protected abstract bool OutOfOrder(int index);
}
```

Agora que BubbleSorter é uma classe abstrata e está aberta para ser herdada, várias classes derivadas podem ser criadas com o intuito de ordenar não apenas arrays de inteiros, mas de qualquer tipo desejado.

``` csharp
public class IntBubbleSorter : BubbleSorter
{
    private int[] array = null;
    public int Sort(int[] theArray)
    {
        array = theArray;
        length = array.Length;
        return DoSort();
    }
    protected override bool OutOfOrder(int index)
    {
        return array[index] > array[index + 1];
    }
    protected override void Swap(int index)
    {
        int temp = array[index];
        array[index] = array[index + 1];
        array[index + 1] = temp;
    }
}
```

``` csharp
public class DoubleBubbleSorter : BubbleSorter
{
    private double[] array = null;
    public int Sort(double[] theArray)
    {
        array = theArray;
        length = array.Length;
        return DoSort();
    }
    protected override bool OutOfOrder(int index)
    {
        return array[index] > array[index + 1];
    }
    protected override void Swap(int index)
    {
        double temp = array[index];
        array[index] = array[index + 1];
        array[index + 1] = temp;
    }
}
```

Contudo, o uso do Template Method traz algumas desvantagens. Por se basear na herança, as classes derivadas acabam ficando muito acopladas em relação às superclasses. Os métodos OutOfOrder e Swap seguem o mesmo princípio em outros algoritmos de ordenação, mas não podem ser reaproveitados desta forma.

## Strategy

O design pattern Strategy se propõe a resolver os mesmos problemas do Template Method, porém de uma maneira totalmente distinta. Para compreendermos isso, vamos observar o exemplo da classe BubbleSorter, desta vez construída sob o prisma do padrão Strategy.

``` csharp
public class BubbleSorter
{
    private int operations = 0;
    private int length = 0;
    private readonly ISortHandler itsSortHandler = null;
    public BubbleSorter(ISortHandler sortHandler)
    {
        itsSortHandler = sortHandler;
    }
    public int Sort(object array)
    {
        itsSortHandler.SetArray(array);
        length = itsSortHandler.Length();
        operations = 0;
        if (length <= 1)
            return operations;
        for (int nextToLast = length - 2; nextToLast >= 0; nextToLast--)
        {
            for (int index = 0; index <= nextToLast; index++)
            {
                if (itsSortHandler.OutOfOrder(index))
                {
                    itsSortHandler.Swap(index);
                }
                operations++;
            }
        }
        return operations;
    }
}
```

``` csharp
public interface ISortHandler
{
    void Swap(int index);
    bool OutOfOrder(int index);
    int Length();
    void SetArray(object array);
}
```

``` csharp
public class IntSortHandler : ISortHandler
{
    private int[] array = null;
    public int Length()
    {
        return array.Length;
    }
    public bool OutOfOrder(int index)
    {
        return array[index] > array[index + 1];
    }
    public void SetArray(object array)
    {
        this.array = (int[])array;
    }
    public void Swap(int index)
    {
        int temp = array[index];
        array[index] = array[index + 1];
        array[index + 1] = temp;
    }
}
```

Em oposição ao que foi retratado com o Template Method, IntSortHandler não tem qualquer conhecimento de BubbleSorter, e portanto não depende do algoritmo em questão.

A estrutura do pattern Template Method desobedece parcialmente o princípio da Inversão de Dependência, pois Swap e OutOfOrder dependem da ordenação por bolhas. Por outro lado, a partir da aplicabilidade de Strategy, IntSortHandler pode ser consumido por outros algoritmos de ordenação.

A afirmação do último parágrafo pode ser comprovada através de mais um exemplo. Imagine que um programa necessite de um algoritmo de ordenação por bolhas otimizado. Ao implementar essa classe, é possível reaproveitarmos a instância de IntSortHandler no novo algoritmo proposto.

``` csharp
public class QuickBubbleSorter
{
    private int operations = 0;
    private int length = 0;
    private ISortHandler itsSortHandler = null;
    public QuickBubbleSorter(ISortHandler sortHandler)
    {
        itsSortHandler = sortHandler;
    }
    public int Sort(object array)
    {
        itsSortHandler.SetArray(array);
        length = itsSortHandler.Length();
        operations = 0;
        if (length <= 1)
            return operations;
        bool thisPassInOrder = false;
        for (int nextToLast = length - 2; nextToLast >= 0 && !thisPassInOrder; nextToLast--)
        {
            thisPassInOrder = true;
            for (int index = 0; index <= nextToLast; index++)
            {
                if (itsSortHandler.OutOfOrder(index))
                {
                    itsSortHandler.Swap(index);
                    thisPassInOrder = false;
                }
                operations++;
            }
        }
        return operations;
    }
}
```

## Template Method ou Strategy?

É natural que depois de conhecer cada um dos padrões, vocês estejam se perguntando qual dos dois utilizar em uma solução do dia a dia. Para responder a essa última pergunta, deixo logo a seguir a conclusão de Uncle Bob a respeito do dilema entre Template Method e Strategy.

[![Readme Quotes](https://quotes-github-readme.vercel.app/api?type=horizontal&theme=dark&quote=Template%20Method%20%C3%A9%20simples%20de%20escrever%20e%20utilizar%2C%20mas%20tamb%C3%A9m%20%C3%A9%20r%C3%ADgido.%20Strategy%20%C3%A9%20flex%C3%ADvel%2C%20mas%20voc%C3%AA%20precisa%20criar%20uma%20classe%20extra%2C%20instanciar%20um%20objeto%20extra%20e%20ligar%20o%20objeto%20ao%20sistema.%20A%20escolha%20depende%20de%20voc%C3%AA%20precisar%20da%20flexibilidade%20de%20Strategy%20ou%20poder%20conviver%20com%20a%20simplicidade%20de%20Template%20Method.&author=Robert%20C.%20Martin)](https://github.com/piyushsuthar/github-readme-quotes)

[(Voltar para o topo)](#saiba-mais-sobre-os-design-patterns)

# Façade

A proposta do padrão Façade é estabelecer uma interface simplificada e específica para os seus clientes, a partir de um grupo de objetos que sejam mais complexos. Podemos notar isso observando a classe Db, implementada por Robert Martin. Ela publica uma API extremamente fácil de se entender e utilizar, direcionada apenas para o tipo ProductData, que por de baixo dos panos consome as dependências da biblioteca System.Data.


``` csharp
public class Db
{
    private static SqlConnection connection;
    public static void Init()
    {
        string connectionString = "Initial Catalog=QuickyMart;Data Source=marvin;User Id=sa;Password=abc;";
        connection = new SqlConnection(connectionString);
        connection.Open();
    }
    public static void Store(ProductData pd)
    {
        SqlCommand command = BuildInsertionCommand(pd);
        command.ExecuteNonQuery();
    }
    private static SqlCommand BuildInsertionCommand(ProductData pd)
    {
        string sql = "INSERT INTO Products VALUES (@sku, @name, @price)";
        SqlCommand command = new(sql, connection);
        command.Parameters.Add(new SqlParameter("@sku", pd.sku));
        command.Parameters.Add(new SqlParameter("@name", pd.name));
        command.Parameters.Add(new SqlParameter("@price", pd.price));
        return command;
    }
    public static ProductData GetProductData(string sku)
    {
        SqlCommand command = BuildProductQueryCommand(sku);
        IDataReader reader = ExecuteQueryStatement(command);
        ProductData pd = ExtractProductDataFromReader(reader);
        reader.Close();
        return pd;
    }
    private static ProductData ExtractProductDataFromReader(IDataReader reader)
    {
        ProductData pd = new();
        pd.sku = reader["sku"].ToString();
        pd.name = reader["name"].ToString();
        pd.price = Convert.ToInt32(reader["price"]);
        return pd;
    }
    private static IDataReader ExecuteQueryStatement(SqlCommand command)
    {
        IDataReader reader = command.ExecuteReader();
        reader.Read();
        return reader;
    }
    private static SqlCommand BuildProductQueryCommand(string sku)
    {
        string sql = "SELECT * FROM Products Where sku = @sku";
        SqlCommand command = new(sql, connection);
        command.Parameters.Add(new SqlParameter("@sku", sku));
        return command;
    }
    public static void DeleteProductData(string sku)
    {
        BuildProductDeleteStatement(sku).ExecuteNonQuery();
    }
    private static SqlCommand BuildProductDeleteStatement(string sku)
    {
        string sql = "DELETE from Products Where sku = @sku";
        SqlCommand command = new(sql, connection);
        command.Parameters.Add(new SqlParameter("@sku", sku));
        return command;
    }
    public static void Close()
    {
        connection.Close();
    }
}
```

A beleza do pattern está em ocultar toda a dificuldade imposta pelo namespace System.Data, de modo que quem chama os métodos públicos da classe Db não tem qualquer conhecimento de como a conexão com o banco de dados está sendo feita.

[![Readme Quotes](https://quotes-github-readme.vercel.app/api?type=horizontal&theme=dark&quote=Uma%20fachada%20como%20Db%20imp%C3%B5e%20muitas%20diretivas%20em%20rela%C3%A7%C3%A3o%20ao%20uso%20de%20System.Data%2C%20sabendo%20como%20inicializar%20e%20fechar%20a%20conex%C3%A3o%20com%20o%20banco%20de%20dados%2C%20transformar%20os%20membros%20de%20ProductData%20em%20campos%20de%20banco%20de%20dados%20e%20desfazer%20essa%20transforma%C3%A7%C3%A3o.&author=Robert%20C.%20Martin)](https://github.com/piyushsuthar/github-readme-quotes)

Ao escolher este padrão para desenvolver a classe Db, o autor do código conscientemente definiu um contrato para se conectar com o banco de dados da aplicação. Caso algum trecho faça a conexão com System.Data sem intermediários, violará a convenção estipulada. Assim sendo, é possível afirmar que o design pattern Façade institui as suas diretivas no programa em questão.

# Mediator

Do mesmo jeito que o padrão Façade, o Mediator impõe diretivas que devem ser seguidas pelo código cliente. Entretanto, há duas diferenças claras entre esses patterns.

A primeira delas é que o Mediator define as suas regras de forma oculta, enquanto o Façade faz isto explicitamente. Já a segunda distinção está relacionada com as restrições causadas pelos padrões. O Mediator não restringe o consumidor que utilizá-lo como dependência, embora o Façade funcione de maneira oposta, trazendo restrições.

``` csharp
public class QuickEntryMediator
{
    private readonly TextBox itsTextBox;
    private readonly ListBox itsList;
    public QuickEntryMediator(TextBox itsTextBox, ListBox itsList)
    {
        this.itsTextBox = itsTextBox;
        this.itsList = itsList;
        itsTextBox.TextChanged += new EventHandler(TextFieldChanged);
    }
    private void TextFieldChanged(object source, EventArgs args)
    {
        string prefix = itsTextBox.Text;
        if (prefix.Length == 0)
        {
            itsList.ClearSelected();
            return;
        }
        ListBox.ObjectCollection listItems = itsList.Items;
        bool found = false;
        for (int i = 0; !found && i < listItems.Count; i++)
        {
            object o = listItems[i];
            string s = o.ToString();
            if (s.StartsWith(prefix))
            {
                itsList.SetSelected(i, true);
                found = true;
            }
        }
        if (!found)
        {
            itsList.ClearSelected();
        }
    }
}
```

Para compreendermos com maior precisão o mecanismo do padrão Mediator, podemos utilizar como base o exemplo acima, também elaborado por Robert Martin.

A classe QuickEntryMediator recebe em seu construtor dois componentes da biblioteca Windows Forms: TextBox e ListBox. No ato de sua instanciação, um EventHandler é registrado na variável TextBox. Quando uma alteração no texto desse componente acontecer, o método TextFieldChanged será invocado. Essa função se responsabiliza por encontrar e selecionar um item do ListBox que tenha em seu prefixo o texto digitado no TextBox.

Percebam que os clientes de ListBox e TextBox não possuem conhecimento deste objeto Mediator. Ele é criado no início do programa, e a partir deste momento monitora quaisquer mudanças realizadas no TextBox, impondo portanto as suas diretivas silenciosamente.

## Escolhendo entre Façade e Mediator

Tenho quase certeza que uma das principais dúvidas de quem está conhecendo estes design patterns é qual dos dois escolher quando for necessário criar diretivas em uma aplicação. Para solucionar esse questionamento, nada melhor do que recorrer às sugestões de Uncle Bob em seu livro:

[![Readme Quotes](https://quotes-github-readme.vercel.app/api?type=horizontal&theme=dark&quote=A%20imposi%C3%A7%C3%A3o%20de%20diretivas%20pode%20ser%20feita%20de%20cima%20com%20Fa%C3%A7ade%2C%20caso%20precisem%20ser%20grandes%20e%20vis%C3%ADveis.%20Caso%20seja%20preciso%20sutileza%20e%20discri%C3%A7%C3%A3o%2C%20o%20Mediator%20%C3%A9%20a%20melhor%20escolha.%20As%20fachadas%20s%C3%A3o%20o%20ponto%20focal%20de%20uma%20conven%C3%A7%C3%A3o.%20Mediadores%20ficam%20ocultos%20dos%20usu%C3%A1rios.%20Sua%20diretiva%20%C3%A9%20um%20fato%20consumado%2C%20em%20vez%20de%20uma%20conven%C3%A7%C3%A3o.&author=Robert%20C.%20Martin)](https://github.com/piyushsuthar/github-readme-quotes)

[(Voltar para o topo)](#saiba-mais-sobre-os-design-patterns)

# Singleton e Monostate

Em grande parte dos programas que escrevemos, muitas instâncias das nossas classes são criadas, e posteriormente descartadas quando cumprem o seu propósito dentro de uma funcionalidade.

Contudo, dependendo da situação, pode ser que a instanciação de múltiplos objetos não seja o ideal, ou até mesmo permitida dentro do contexto de um novo desenvolvimento. Nesses casos, devemos limitar que determinadas classes possuam somente uma instância, desde o início até o fim da execução da aplicação.

Geralmente, objetos que exigem essa premissa são fábricas, utilizadas para criar outros objetos do sistema. Em outros momentos, os objetos em questão são gerenciadores, que monitoram e orquestram uma quantidade considerável de dependências.

Existe um mecanismo chamado Just Create One, que simplifica a necessidade de instanciar um único objeto de uma classe. Nesse padrão, o desenvolvedor, de fato, cria apenas uma instância do objeto no seu programa e se da por satisfeito. Essa abordagem, porém, transmite pouca expressividade no código, e não deixa claro para os demais programadores que outros objetos não devem ser construídos.

Para resolver esse problema, dois design patterns foram elaborados para impor a singularidade de objetos: Singleton e Monostate. Embora possuam o mesmo objetivo, as suas implementações são completamente distintas, e trazem vantagens e desvantagens particulares.

## Singleton

O padrão de projeto Singleton é um dos mais fáceis de se implementar. Para comprovar isso, podemos observar um dos exemplos programados por Robert C. Martin.

``` csharp
public class Singleton
{
    private static Singleton theInstance = null;
    
    private Singleton() { }
    
    public static Singleton Instance
    {
        get
        {
            if (theInstance == null)
            {
                theInstance = new Singleton();
            }
            return theInstance;
        }
    }
}
```

A instância da classe Singleton pode ser acessada com a propriedade pública Instance. Se Instance for chamada várias vezes, uma referência para a mesma instância será retornada, sem exceções. A classe não possui construtores públicos, logo não há como criar um objeto sem usar a propriedade já mencionada.

Uncle Bob destaca as seguintes vantagens de usar o padrão Singleton:

- **Suporte a Lazy Loading**: caso a classe Singleton não seja invocada, a instância jamais será criada;
- **Suporte a derivação**: dada qualquer classe, é possível implementar uma subclasse Singleton;
- **Suporte a qualquer classe**: se for preciso, qualquer classe pode ser transformada em um Singleton, bastando tornar os construtores privados e incluir os métodos/propriedades estáticas requeridas.

Assim como qualquer pattern, Singleton também possui os seus custos:

- **Eficiência**: sempre que a propriedade Instance é chamada, a instrução if acaba sendo verificada. Isso caracteriza um gasto desnecessário de processamento (mesmo que baixo), pois para a maioria dos cenários, a checagem não terá mais utilidade;
- **Falta de transparência**: necessariamente, todos os usuários de uma classe Singleton sabem que estão consumindo este padrão, dado que a propriedade Instance deve ser usada;
- **Destruição indefinida**: objetos Singleton são difíceis de serem destruídos, ou seja, liberados da memória. Por exemplo, se o programador incluir um método que anula o campo theInstance, outras classes ainda podem referenciar o Singleton. Logo, outras invocações para Instance criarão mais uma instância, duplicando-a.

## Monostate

O design pattern Monostate atua de forma diferente do Singleton para alcançar a singularidade de objetos. A seguir temos um trecho de código criado por Martin para ilustrar o mecanismo do padrão.

``` csharp
public class Monostate
{
    private static int itsX;
    
    public int X
    {
        get { return itsX; }
        set { itsX = value; }
    }
}
```

Conforme a definição da classe Monostate, percebe-se que os objetos instanciados compartilharão a variável itsX. Isso porque ela foi declarada internamente como estática.

A distinção em relação à Singleton está no comportamento x estrutura. Enquanto Singleton força a estrutura da singularidade, impedindo que mais instâncias sejam criadas, Monostate impõe o comportamento da singularidade, permitindo a instanciação de vários objetos.

Robert Martin ressalta os benefícios de usar Monostate:

- **Transparência**: os clientes da classe Monostate se comportam do mesmo jeito que os clientes de classes que não utilizam o padrão;
- **Derivação**: as subclasses de um Monostate também são Monostate. Todas compartilham as mesmas variáveis estáticas;
- **Polimorfismo**: os métodos de um Monostate não são estáticos, habilitando a sobrescrita de funções;
- **Destruição definida**: opondo-se ao Singleton, as variáveis de um Monostate possuem o seu ciclo de vida (criação e destruição) bem definido.

As desvantagens da aplicabilidade do padrão estão listadas abaixo:

- **Ausência de conversão**: classes que não são Monostate não podem ser convertidas em Monostate a partir de derivação;
- **Eficiência**: tendo em vista que Monostate habilita a criação e destruição de múltiplas instâncias, isto pode ter um custo considerável durante a execução do sistema;
- **Memória**: independente da utilização de um Monostate, as suas variáveis irão ocupar espaço.

## Escolhendo entre Singleton e Monostate

De acordo com o funcionamento dos padrões Singleton e Monostate, qual dos dois escolher quando for preciso impor a singularidade de objetos?

Robert C. Martin deu a resposta para esta pergunta na sua obra:

[![Readme Quotes](https://quotes-github-readme.vercel.app/api?type=horizontal&theme=dark&quote=O%20melhor%20uso%20do%20Singleton%20se%20d%C3%A1%20quando%20voc%C3%AA%20tem%20uma%20classe%20que%20precisa%20restringir%20por%20deriva%C3%A7%C3%A3o%20e%20n%C3%A3o%20se%20importa%20que%20todos%20tenham%20que%20chamar%20a%20propriedade%20Instance.%20O%20melhor%20uso%20do%20Monostate%20se%20d%C3%A1%20quando%20voc%C3%AA%20quer%20que%20a%20singularidade%20da%20classe%20seja%20transparente%20para%20os%20usu%C3%A1rios%20ou%20quando%20quer%20usar%20derivadas%20polim%C3%B3rficas%20do%20%C3%BAnico%20objeto.&author=Robert%20C.%20Martin)](https://github.com/piyushsuthar/github-readme-quotes)

[(Voltar para o topo)](#saiba-mais-sobre-os-design-patterns)

# Null Object

A principal proposta do design pattern Null Object pode ser compreendida a partir de um breve trecho de código desenvolvido por Robert C. Martin. Vamos conferir.

``` csharp
Employee e = DB.GetEmployee("Bob");
if (e != null && e.IsTimeToPay(today))
  e.Pay();
```

A primeira linha de código do exemplo anterior busca a instância de um objeto do tipo Employee do banco de dados. Neste caso, se não houver um funcionário registrado com o nome ‘Bob’, o método GetEmployee retornará null. Para que a função Pay seja chamada, o funcionário deve existir e estar apto a receber um pagamento.

Qualquer programador que trabalhe com Java, C# e outras linguagens orientadas a objeto, provavelmente escreveu um código parecido com este que acabou de ser demonstrado. Digo isso porque é comum verificarmos se uma instância está nula antes de utilizá-la, de modo a evitar o lançamento de exceções.

Contudo, mesmo com a evolução das features disponibilizadas pelas novas versões das linguagens de programação que auxiliam na checagem de valores nulos, muitos desenvolvedores ainda sofrem com os famosos erros advindos da NullReferenceException. Além disso, a própria verificação se um objeto está nulo polui o código-fonte, dificultando a sua legibilidade.

Há programadores que julguem ser uma opção melhor lançar exceções explicitamente no lugar de retornar objetos nulos. Todavia, esse tipo de solução não costuma ter um impacto positivo, pois os blocos try/catch também são deselegantes, e podem diminuir a eficiência do código.

O padrão de projetos Null Object desponta como uma das melhores saídas para o cenário abordado até então. Esse pattern acaba com a obrigatoriedade de checar se um objeto está nulo, além de não exigir o tratamento defensivo de exceções. Logo, ele simplifica o código elaborado pelos desenvolvedores, tornando-o mais limpo.

## Implementando o padrão

Da mesma forma que o padrão Null Object proporciona a simplificação do código, a sua implementação não deixa a desejar, sendo portanto bastante descomplicada. Para comprovar isso, mostro a seguir o diagrama de classes criado por Martin, que explica o funcionamento do design pattern.

<p align="center">
<img width="600" src="https://miro.medium.com/max/828/1*hn_EcZgxiCRdpzVzXRXMNQ.webp" alt="Diagrama de classes ilustrando os mecanismos do padrão Null Object">
</p>

Conforme pode ser observado, Employee é adaptada para uma interface, que possui duas implementações. A classe EmployeeTransaction é a implementação padrão do objeto. Assim sendo, quando a classe DB localizar um funcionário no banco de dados, retornará uma instância desse tipo.

Por outro lado, se DB não encontrar o funcionário pesquisado, agora com a nova estrutura do padrão Null Object, o método GetEmployee retornará uma instância de NullEmployee. Esse tipo implementa os mesmos métodos de EmployeeTransaction, com a diferença que eles não fazem absolutamente nada.

Com as mudanças aplicadas por Null Object, o primeiro trecho de código incluído no artigo pode ficar da seguinte maneira:

``` csharp
Employee e = DB.GetEmployee("Bob");
if (e.IsTimeToPay(today))
  e.Pay();
```

Diferente da alternativa de lançar exceções para funcionários não localizados, esta abordagem é visualmente agradável e sem propensão a erros inesperados. O método GetEmployee sempre irá retornar uma instância de Employee, que por sua vez se comportará corretamente, independente de ter sido buscada com sucesso do banco de dados.

``` csharp
 public abstract class Employee
 {
     public abstract bool IsTimeToPay(DateTime time);
     
     public abstract void Pay();
     
     public static readonly Employee NULL = new NullEmployee();
     
     private class NullEmployee : Employee
     {
         public override bool IsTimeToPay(DateTime time)
         {
             return false;
         }
         
         public override void Pay()
         {
         }
     }
 }
```

O código da classe abstrata Employee está exposto na imagem acima. Ela possui uma variável estática chamada NULL, que armazena a única instância da classe aninhada NullEmployee.

A estratégia de definir NullEmployee como um tipo privado aninhado é interessante. Isso garante que apenas uma instância dela irá existir por todo o sistema, remetendo ao padrão Singleton.

A variável estática NULL serve como uma referência para os consumidores de Employee que precisarem saber se um funcionário foi encontrado pelo método GetEmployee.

``` csharp
Employee employee = DB.GetEmployee("Bob");
if (employee == Employee.NULL) {
  //lógica para um funcionário não encontrado
}
```

## O poder de Null Object

Uncle Bob destaca as vantagens oportunizadas pelo padrão Null Object:

[![Readme Quotes](https://quotes-github-readme.vercel.app/api?type=horizontal&theme=dark&quote=Aqueles%20que%20t%C3%AAm%20usado%20linguagens%20baseadas%20em%20C%20se%20acostumaram%20com%20fun%C3%A7%C3%B5es%20que%20retornam%20null%20ou%200%20em%20casos%20de%20falha.%20Presumimos%20que%20o%20retorno%20de%20tais%20fun%C3%A7%C3%B5es%20precisa%20ser%20testado.%20Null%20Object%20muda%20isso.%20Com%20esse%20padr%C3%A3o%2C%20garantimos%20que%20as%20fun%C3%A7%C3%B5es%20sempre%20retornem%20objetos%20v%C3%A1lidos%2C%20mesmo%20quando%20falham.&author=Robert%20C.%20Martin)](https://github.com/piyushsuthar/github-readme-quotes)

[(Voltar para o topo)](#saiba-mais-sobre-os-design-patterns)

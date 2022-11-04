# Design Patterns por Robert Martin

Repositório com o objetivo de compartilhar exemplos de design patterns elaborados por Robert C. Martin em seu livro *Princípios, Padrões e Práticas Ágeis em C#*.

# Índice

- [Command](#command)
  - [Command e a simplicidade](#command-e-a-simplicidade)
  - [Utilizando Command na vida real](#utilizando-command-na-vida-real)
  - [Command e transações](#command-e-transações)
  - [O método Undo](#o-método-undo)
  - [1001 utilidades](#1001-utilidades)
- [Template Method e Strategy](#template-method-e-strategy)

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

[(Voltar para o topo)](#índice)

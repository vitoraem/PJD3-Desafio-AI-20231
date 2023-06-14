## Desafio #0X AI Tournament

Desenvolver inteligência artificial para jogo ***Capture The Flag***.
A partir do projeto LINK que utiliza Finite State Machine (Máquina de Estado), implementar apenas os *State* (Estados de comportamento).
#### State
- Todos os estados implementados devem derivar a class **State**;
- Pode-se implementar quantos estados forem necessários;
- Implementar todos os states no mesmo arquivo;
#### FSMInitializer
- Implementar class inicializadora derivando a class **FSMInitializer**
- Implementar override no Name, determinando o nome do Jogador;
- Implementar override no método Init e registrar os States criados.
- Assinatura do método RegisterState (T é o tipo do State): `void RegisterState<T>(string name)`
```csharp
using ChallengeAI;
public class FSMInitExample : FSMInitializer
{
  public override string Name => "Example";
  public override void Init()
  {
    RegisterState<IdleTestState>("IDLE");
    RegisterState<CaptureFlagTest>("CAPTURE_FLAG");
    RegisterState<WalkToFlagTest>("WALK_FLAG");
    RegisterState<WalkToRandomTest>("WALK_RANDOM");
  }
}
```
Criar repositório no GitHub para o projeto. Subir para o repositório apenas as pastas Assets, Packages, ProjectSettings e UserSettings.


Postar no Blackboard apenas os arquivos dos States e no FSMInitializer.

### Requisitos
* **Scenes 3D**
    Podem ser criadas utilizando o ProBuilder;
    Devem possuir configuração de luz adequada;
    O layout da scene deve contemplar as funcionalidades dos requisitos;
    Colisores devem estar de acordo com o level;
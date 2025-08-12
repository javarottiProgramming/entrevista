# 👩‍💻 Projeto Entrevista

Este projeto destina-se a entrevistar os candidatos a vaga de desenvolvedor de software na empresa EVUP.

## Instruções

### 1 📋 Pré-requisitos

Antes de começar, certifique-se de que tenha instalado os programas a seguir:

1.1- GIT: https://git-scm.com/downloads

1.2- Visual Studio (2019 ou superior): https://visualstudio.microsoft.com/pt-br/downloads/

### 2 ⚙ Baixando o código fonte

2.1- Crie uma pasta no seu computador onde o código fonte será baixado (ex. C:/EVUP);

2.2- Abra o **cmd**, navegue até a pasta que criou e faça o clone do projeto atual nessa pasta (pode usar o Visual Studio ou usar a linha de comando a seguir):
```
git clone https://github.com/evuptech/entrevista.git
```

O resultado deverá ser algo parecido com este:

![image](https://user-images.githubusercontent.com/94454745/142252376-995954d9-6bd5-4c71-a9fd-0fce99e6cbf9.png)

2.3- navegue na pasta "./entrevista" (que acabou de ser criada pelo comando acima) e crie um novo branch com seu nome:
```
git branch candidato/seu-nome
```

2.3.1- Faça o checkout do branch para trabalhar nele:
```
git checkout candidato/seu-nome
```

2.4- Após baixar o projeto, abra a solution no Visual Studio que fica no caminho: "./entrevista/Projeto/Projeto.sln".

![image](https://user-images.githubusercontent.com/94454745/142243975-8056f9f2-9e16-40a9-8fdc-b149cb3f17d5.png)

2.5- Execute o projeto.

![image](https://user-images.githubusercontent.com/94454745/142244048-2b7dfeb7-de94-4b1f-ba7d-d2a6a2ff91c1.png)

O resultado esperado é o projeto rodando:

![image](https://user-images.githubusercontent.com/94454745/142245506-3d4385bc-e30d-4310-9e5c-2fc3fed8aba7.png)

Na tela acima, informe o usuário e senha (admin/admin) para entrar e ter acesso a lista de tarefas do teste.

### 3 ✅ Entregando o teste

Ao final do teste, quando terminar de programar, faça o commit de todas as suas alterações (pode usar o Visual Studio ou por linha de comando, conforme a seguir):

3.1- Abra o **cmd** e navegue até a pasta "./entrevista";

3.2- Adicione todos os arquivos modificados para fazer o commit:

```
git add .
```

3.3- Efetue o comando do commit:
```
git commit -m "Entregando o teste"
```

Agora, temos duas maneiras de entregar seu código fonte para ser analisado (submetendo seu branch para o servidor ou enviando os arquivos compactados para o analista da entrevista).

#### 3.4 🔓 Submetendo seu branch para o servidor remoto

3.4.1- Para enviar seu branch para o servidor, você precisa primeiro ter um e-mail cadastrado no github (Pode ser criado por esse link: https://github.com/signup).

3.4.2- Depois esse e-mail precisa ter sido adicionado ao repositório por um administrador (solicite para a pessoa que lhe enviou o teste o acesso, passando seu e-mail para ela).

3.4.3- Faça login no github em seu ambiente de trabalho local com o e-mail cadastrado. O comando para login é o seguinte:

PS: caso não tenha instalado o Github CLI na sua máquina, pode seguir as instruções desse link: https://docs.github.com/pt/get-started/getting-started-with-git/caching-your-github-credentials-in-git)
```
gh auth login
```
(siga o passo-a-passo até concluit o login).


3.4.4- Faça o push do seu branch para nosso repositório:
```
git push --set-upstream origin candidato/seu-nome
```

O resultado deverá ser algo parecido com este:

![image](https://user-images.githubusercontent.com/94454745/142252583-01af8fc3-eb4d-4a4d-9ad5-45e681f81a4a.png)




3.4.5- E por último faça o push para enviar seu código ao servidor:

```
git push
```

O resultado esperado de todos os comando acima:

![image](https://user-images.githubusercontent.com/94454745/142252906-54b934b5-2035-412d-9140-916d8eedfbc6.png)

#### 3.5🗜️ Enviando a pasta do projeto compactada como zip

3.5.1- Compacte a pasta ./entrevista com todos os arquivos dentro (pode usar a extensão .zip ou .rar)

3.5.2- Envie o arquivo compactado para a mesma pessoa que te entregou o link deste repositório.

FIM

# ₿ Bitcoin Price Checker - .NET 8 Web API
Este projeto é uma Web API moderna construída com .NET 8 para monitorar em tempo real o preço do Bitcoin utilizando a API Rest da CoinGecko.

O projeto foi desenvolvido para demonstrar a aplicação de padrões corporativos do ecossistema .NET, focando em escalabilidade, resiliência e boas práticas de infraestrutura.

## 🛠️ Stack Técnica & Padrões
- .NET 8 (Minimal APIs): Abordagem moderna para criação de serviços leves e performáticos.

- Dependency Injection (DI): Utilização do container nativo do ASP.NET para gestão de ciclo de vida de serviços.

- Typed HttpClientFactory: Implementação otimizada para consumo de APIs externas, evitando o esgotamento de sockets e permitindo a configuração de resiliência (User-Agent, Headers).

- Service Layer Pattern: Separação clara entre a exposição da rota e a lógica de integração/negócio.

- Strongly Typed Models: Uso de Records para imutabilidade e mapeamento seguro de JSON.

- Docker (Multi-stage Build): Criação de imagens leves baseadas em Alpine Linux, com suporte a globalização (ICU) configurado.

- Structured Logging: Monitoramento via ILogger integrado para rastreabilidade de erros em produção.

## 📦 Estrutura do Projeto

```├── Models/             # Records e DTOs (Data Transfer Objects)
├── Services/           # Lógica de integração e regras de negócio
├── Program.cs          # Configuração da aplicação e endpoints
├── Dockerfile          # Build multi-estágio focado em segurança e tamanho
└── .gitignore          # Configuração específica para ambientes .NET
```

## 🚀 Como Executar?
```
docker build -t btc-checker .
docker run -d -p 5000:8080 --name btc-app btc-checker
```

## 🤖 Como Testar?
```
curl http://localhost:5000/get-bitcoin-price
```

## 🧠 Desafios Superados (Perspectiva de Engenharia)
- Vindo de uma base em Rust e Node.js, este projeto aplicou conceitos avançados que garantem a prontidão para produção:

- Resiliência em APIs: Tratamento de erros HTTP e simulação de User-Agent para evitar bloqueios por segurança (403 Forbidden).

- Otimização de Container: Resolução do erro de Globalization Invariant Mode no Alpine Linux, instalando bibliotecas ICU e configurando variáveis de ambiente para suporte a culturas específicas (en-US).

- Injeção de Dependência: Migração de lógica procedural para uma arquitetura desacoplada, facilitando testes unitários futuros.
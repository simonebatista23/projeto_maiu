# 📊 Painel Administrativo — Gráfico Geral de Chamados (C#)

Este projeto é um painel administrativo que consome a **API de Chamados Internos** para exibir um gráfico geral com a quantidade total de chamados do sistema. O gráfico mostra todos os chamados registrados, permitindo acompanhar a movimentação diária. A tela é exclusiva para administradores e permite filtrar o período por mês. Quando um chamado é criado ou cancelado, o gráfico atualiza automaticamente refletindo a mudança daquele dia.

---

## 🚀 Tecnologias Utilizadas
- **C#**
- **.NET (MAUI ou Blazor, conforme o projeto)**
- **Biblioteca de Gráficos (Microcharts / ChartJS / equivalente)**
- **HttpClient para consumir a API**
- **Autenticação JWT**

---

## ⚙️ Funcionalidades

| Funcionalidade | Descrição |
|----------------|-----------|
| **Acesso exclusivo para Admin** | Apenas administradores podem acessar a tela do gráfico. |
| **Gráfico Geral de Chamados** | Mostra todos os chamados registrados no sistema. |
| **Filtro de Mês** | Permite selecionar qualquer mês e atualizar os dados automaticamente. |
| **Atualização em Tempo Real** | Chamado criado = soma no gráfico / Chamado cancelado = diminui no gráfico. |
| **Consumo da API** | Todos os dados exibidos são obtidos diretamente da API de Chamados Internos. |

---

## 📡 Comunicação com API
O painel consome endpoints para:

- Listar **todos os chamados**  
- Filtrar **por mês**  
- Consultar status (abertos, cancelados e concluídos)

A autenticação é feita via token JWT.

---

## 📊 Como o Gráfico Funciona

- Chamado criado no dia → **+1** no gráfico  
- Chamado cancelado no dia → **-1** no gráfico  
- Alterar o mês → Redesenha o gráfico com novas informações  
- Sempre mostra **todos os chamados do sistema** no período selecionado  

---

## ▶️ Como Executar
1. Configurar o projeto no **MAUI**  
2. Certificar-se de que a **API está rodando**  
3. Definir a URL da API nas configurações  
4. Executar o projeto  
5. Fazer login como **Administrador**  

---

## ✔️ Conclusão
Este painel oferece uma visão clara e objetiva da quantidade total de chamados no sistema. Com filtro mensal e atualização instantânea, permite ao administrador acompanhar facilmente toda a movimentação diária de chamados.

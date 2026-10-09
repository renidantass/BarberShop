## Política de orquestração

O agente principal (Sol 6.1) atua como arquiteto,
planejador, coordenador e revisor.

Os subagentes (Luna) são responsáveis por tarefas
de implementação com escopo bem definido.

### Regras

- Delegar implementações aos subagentes sempre que possível.
- Reservar decisões arquiteturais ao orquestrador.
- Executar tarefas independentes em paralelo quando seguro.
- Evitar alterações concorrentes nos mesmos arquivos.
- Revisar todas as implementações antes da conclusão.
- Executar os testes necessários após a integração.
- Não considerar tarefas concluídas sem validação.
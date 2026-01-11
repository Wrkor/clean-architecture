namespace Api.Swagger.Filters;

public class DescriptionFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument document, DocumentFilterContext context)
    {
        document.Info = new OpenApiInfo()
        {
            Title = "REST API | Социальная сеть",
            Description =
                @"<h1>Together Hub</h1>
<h2> Техническое задание №4. Кастомные изменения в проекте:</h2>
<ul>
    <li>Обновил все проекты до .net 10 + все nuget пакеты (кроме mediatr, спасибо лицензированию)</li>
    <li>Изменен Automapper => Mapster (когда лицензция изменилась перешел на mapster, решил попрактиковаться с ним и здесь)</li>
    <li>Добавлен Swagger и фильтр описания для него (Api/Swagger/Filters)</li>
    <li>DI в API разнес по разным методам расширения (имхо более читаймо стало)</li>
    <li>Переименовал в Domain и Application папки Security => Identity (имхо более читаймо стало)</li>
    <li>Реализация интерфейса JwtService вынесена в Infrastructure (я всегда выношу реализации сервисов в инфраструктуры, ведь это логично, в ней нету никакой бизнес логики, а только фактические провайдеры данных)</li>
    <li>Не стал добавлять ValidationMiddleware - имхо, плохой подход в разработке механизма валидации, так как несет много избыточного кода + протаскивать каждый запрос по всем middlewares не есть хорошо для оптимизации. Я преверженец валидации в behaviour в пайплайне запроса через медиатора (дальше вы как раз и разбираете данный подход с FluentValidation)</li>
    <li>Реализация команды RegisterUserCommand и запроса LoginUserQuery находится в Application/Identity/</li>
</ul>
<p>Это шаблон проекта, который будет разрабатываться в рамках курса на платформе Stepik: <a href='https://stepik.org/a/229549/pay?promo=608773466d844860&utm_source=dhub&utm_medium=git&utm_campaign=sale_start&utm_term=cqrs&utm_content=gh'>присоединяйтесь</a> 🙏</p>

<h2>Следить за акциями и специальными предложениями в <a href='https://t.me/+DUbX9jlu7T0xOTEy'>Telegram</a></h2>

<h3>Для ознакомления:</h3>
<ul>
    <li><a href='https://hub.docker.com/r/iksergey/together-hub'>Docker-образ</a></li>
    <li>Swagger по маршруту <code>http://localhost:&lt;PORT&gt;</code></li>
</ul>

<p><strong>Together Hub</strong> — это платформа для создания и управления топиками с интегрированной системой аутентификации и авторизации, построенная на <strong>.NET</strong>.</p>

<p>В репозитории есть шаблон проекта. При необходимости можете воспользоваться скриптами для автоинициализации всей структуры.</p>

<p>Во избежание проблем при создании проекта убедительная просьба использовать .NET 9. При желании потом можно обновиться до более поздних версий.</p>

<h2>Скрипты для инициализации проекта</h2>
<p>В репозитории доступны два скрипта для автоматической настройки проекта:</p>

<h3>Для Mac/Linux</h3>
<p>Используйте bash-скрипт <code>setup-dotnet-project.sh</code>:</p>

<h3>Для Windows</h3>
<p>Используйте PowerShell-скрипт <code>setup-dotnet-project.ps1</code>:</p>
<p>Разрешить выполнение скриптов (от администратора): <code>Set-ExecutionPolicy RemoteSigned</code></p>

<h2>Что делают скрипты</h2>
<ul>
    <li>Создают структуру проекта</li>
    <li>Добавляют необходимые NuGet-пакеты</li>
    <li>Настраивают связи между проектами</li>
    <li>Инициализируют базу данных</li>
    <li>Создают начальные миграции</li>
</ul>

<h2>Важные замечания</h2>
<ul>
    <li>Для Windows: если возникает ошибка выполнения скрипта, убедитесь что PowerShell запущен с правами администратора</li>
    <li>Для Mac/Linux: если скрипт не запускается, проверьте права на выполнение (<code>chmod +x</code>)</li>
    <li>В обоих случаях должен быть установлен .NET SDK версии 8 или выше</li>
</ul>

<h2>Архитектура проекта</h2>
<p>Проект построен с использованием <strong>Clean Architecture</strong>, применяющей подходы:</p>
<ul>
    <li><strong>CQRS (Command Query Responsibility Segregation)</strong></li>
    <li><strong>Mediator Pattern</strong></li>
    <li><strong>Domain-Driven Design</strong></li>
</ul>

<h3>Слои архитектуры</h3>
<ul>
    <li><strong>Domain</strong>: Содержит основные сущности, value objects и бизнес-правила.</li>
    <li><strong>Application</strong>: Отвечает за бизнес-логику и реализацию процессов, таких как команды и запросы.</li>
    <li><strong>Infrastructure</strong>: Реализует взаимодействие с базой данных, внешними сервисами и поддерживает Identity.</li>
    <li><strong>API</strong>: Включает контроллеры и конфигурацию веб-приложения.</li>
</ul>

<h2>Технологический стек</h2>
<ul>
    <li><strong>Backend</strong>: .NET 9</li>
    <li><strong>ORM</strong>: Entity Framework Core</li>
    <li><strong>База данных</strong>: SQLite → PostgreSQL</li>
    <li><strong>Аутентификация</strong>: ASP.NET Core Identity с поддержкой JWT</li>
    <li><strong>Паттерны</strong>: CQRS, Mediator</li>
    <li><strong>Валидация</strong>: Кастомный middleware для проверки входных данных</li>
</ul>

<h2>Ключевые особенности реализации</h2>
<h3>Особенности реализации</h3>
<ul>
    <li><strong>Использование Value Objects</strong>: Для работы с идентификаторами и значениями, обеспечивая безопасность и неизменяемость данных.</li>
    <li><strong>Поддержка мягкого удаления (Soft Delete)</strong>: Удаленные сущности сохраняются в базе, но становятся недоступными.</li>
    <li><strong>Расширенная обработка исключений</strong>: Централизованный обработчик с формированием стандартизированных JSON-ответов.</li>
    <li><strong>Гибкая система авторизации</strong>: Реализация и настройка политик и требований.</li>
    <li><strong>Автомаппинг DTO и сущностей</strong>: С использованием AutoMapper для упрощения преобразования данных между слоями.</li>
</ul>

<h3>Бизнес-логика</h3>
<ul>
    <li>Регистрация и аутентификация пользователей</li>
    <li>Создание и управление топиками</li>
    <li>Система ролей участников (организатор, спикер, участник)</li>
    <li>Комментирование топиков</li>
</ul>

<h2>Workflow приложения</h2>
<ol>
    <li>Регистрация пользователя</li>
    <li>Получение JWT-токена</li>
    <li>Создание топиков</li>
    <li>Присоединение к топикам</li>
    <li>Добавление комментариев</li>
</ol>

<h2>Преимущества проекта</h2>
<ul>
    <li><strong>Чистая архитектура</strong>: Четкое разделение ответственности между слоями.</li>
    <li><strong>Современные подходы разработки</strong>: Использование CQRS и DDD-подходов.</li>
    <li><strong>Легкость масштабирования</strong>: Благодаря MediatR и независимости слоев.</li>
    <li><strong>Фокус на безопасности</strong>: Использование ASP.NET Identity и JWT.</li>
</ul>

<p><strong>Проект демонстрирует современный подход к разработке backend-приложений с акцентом на чистую архитектуру и принципы Domain-Driven Design</strong></p>",
        };
    }
}

namespace Apteka;

// One line per text:  Add( russian source text (= key),  English,  French,  Arabic );
// {0}, {1} ... are placeholders and must appear in every language.
public static partial class Loc
{
    static Loc()
    {
        // ===== Menus / buttons / labels =====
        Add("Аптека — Информационная система", "Pharmacy — Information System", "Pharmacie — Système d'information", "الصيدلية — نظام المعلومات");
        Add("Аптека — Вход", "Pharmacy — Sign in", "Pharmacie — Connexion", "الصيدلية — تسجيل الدخول");
        // sign-in / change-password forms (style of the teacher's sample)
        Add("Вход", "Sign in", "Connexion", "دخول");
        Add("Имя пользователя", "User name", "Nom d'utilisateur", "اسم المستخدم");
        Add("АИС Аптека", "Pharmacy IS", "SI Pharmacie", "نظام الصيدلية");
        Add("Версия {0}", "Version {0}", "Version {0}", "الإصدار {0}");
        Add("Введите имя пользователя и пароль", "Enter your user name and password", "Saisissez votre nom d'utilisateur et votre mot de passe", "أدخل اسم المستخدم وكلمة المرور");
        Add("Язык ввода: {0}", "Input language: {0}", "Langue de saisie : {0}", "لغة الإدخال: {0}");
        Add("Клавиша CapsLock нажата", "Caps Lock is on", "Verr. Maj. activé", "مفتاح Caps Lock مفعّل");
        Add("Прежний пароль", "Previous password", "Ancien mot de passe", "كلمة المرور السابقة");
        Add("Подтвердите", "Confirm", "Confirmer", "تأكيد");
        Add("Пользователь: {0}", "User: {0}", "Utilisateur : {0}", "المستخدم: {0}");
        Add("Введите прежний и новый пароль", "Enter the previous and the new password", "Saisissez l'ancien et le nouveau mot de passe", "أدخل كلمة المرور السابقة والجديدة");
        // analytics dashboard
        Add("Экспорт дашборда в PNG", "Export dashboard to PNG", "Exporter le tableau de bord en PNG", "تصدير لوحة المعلومات إلى PNG");
        Add("Справочники", "Directories", "Référentiels", "الجداول المرجعية");
        Add("Документы", "Documents", "Documents", "المستندات");
        Add("Оформление заказа", "Place an order", "Passer une commande", "إنشاء طلب");
        Add("Журналы документов", "Document logs", "Journaux de documents", "سجلات المستندات");
        Add("Отчёты и запросы", "Reports and queries", "Rapports et requêtes", "التقارير والاستعلامات");
        Add("Экспорт текущей таблицы в CSV", "Export current table to CSV", "Exporter la table actuelle en CSV", "تصدير الجدول الحالي إلى CSV");
        Add("Аналитика", "Analytics", "Analyses", "التحليلات");
        Add("Справка", "Help", "Aide", "المساعدة");
        Add("Руководство пользователя", "User guide", "Guide de l'utilisateur", "دليل المستخدم");
        Add("О программе", "About", "À propos", "حول البرنامج");
        Add("Разное", "Misc", "Divers", "أخرى");
        Add("Настройка", "Settings", "Paramètres", "الإعدادات");
        Add("Сменить пароль", "Change password", "Changer le mot de passe", "تغيير كلمة المرور");
        Add("Выход", "Log out", "Déconnexion", "خروج");
        Add("Поиск:", "Search:", "Recherche :", "بحث:");
        Add("Добавить", "Add", "Ajouter", "إضافة");
        Add("Изменить", "Edit", "Modifier", "تعديل");
        Add("Удалить", "Delete", "Supprimer", "حذف");
        Add("Пациент", "Patient", "Patient", "المريض");
        Add("Лекарство", "Medicine", "Médicament", "الدواء");
        Add("Количество", "Quantity", "Quantité", "الكمية");
        Add("Оформить заказ", "Place order", "Valider la commande", "تأكيد الطلب");
        Add("Шаблон отчёта:", "Report template:", "Modèle de rapport :", "قالب التقرير:");
        Add("Выполнить", "Run", "Exécuter", "تنفيذ");
        Add("Экспорт в CSV", "Export to CSV", "Exporter en CSV", "تصدير إلى CSV");
        Add("Диаграмма:", "Chart:", "Graphique :", "الرسم البياني:");
        Add("С:", "From:", "Du :", "من:");
        Add("По:", "To:", "Au :", "إلى:");
        Add("Применить фильтр", "Apply filter", "Appliquer le filtre", "تطبيق التصفية");
        Add("Автообновление", "Auto-refresh", "Actualisation auto", "التحديث التلقائي");
        Add("Экспорт графика в PNG", "Export chart to PNG", "Exporter le graphique en PNG", "تصدير الرسم إلى PNG");
        Add("Автообновление: ВЫКЛ", "Auto-refresh: OFF", "Actualisation auto : NON", "التحديث التلقائي: إيقاف");
        Add("Автообновление: ВКЛ", "Auto-refresh: ON", "Actualisation auto : OUI", "التحديث التلقائي: تشغيل");
        Add("Связующие таблицы (M:N)", "Link tables (M:N)", "Tables de liaison (M:N)", "جداول الربط (M:N)");
        Add("Пользователь: {0}   |   Роль: {1}", "User: {0}   |   Role: {1}", "Utilisateur : {0}   |   Rôle : {1}", "المستخدم: {0}   |   الدور: {1}");
        Add("Записей: {0}", "Records: {0}", "Enregistrements : {0}", "عدد السجلات: {0}");
        Add("Строк: {0}", "Rows: {0}", "Lignes : {0}", "عدد الصفوف: {0}");
        Add("Нет данных", "No data", "Aucune donnée", "لا توجد بيانات");
        Add("Нет данных для экспорта", "No data to export", "Aucune donnée à exporter", "لا توجد بيانات للتصدير");
        Add("Данные обновлены: {0:HH:mm:ss}", "Data updated: {0:HH:mm:ss}", "Données actualisées : {0:HH:mm:ss}", "تم تحديث البيانات: {0:HH:mm:ss}");
        Add("Данные обновлены: {0:HH:mm:ss} (авто)", "Data updated: {0:HH:mm:ss} (auto)", "Données actualisées : {0:HH:mm:ss} (auto)", "تم تحديث البيانات: {0:HH:mm:ss} (تلقائي)");

        // ===== Roles =====
        Add("Администратор", "Administrator", "Administrateur", "المدير");
        Add("Фармацевт", "Pharmacist", "Pharmacien", "الصيدلي");
        Add("Гость", "Guest", "Invité", "زائر");

        // ===== Charts =====
        Add("Количество лекарств по категориям", "Medicines count by category", "Nombre de médicaments par catégorie", "عدد الأدوية حسب الفئة");
        Add("Заказы по статусам", "Orders by status", "Commandes par statut", "الطلبات حسب الحالة");
        Add("Средняя цена по категориям", "Average price by category", "Prix moyen par catégorie", "متوسط السعر حسب الفئة");
        Add("Заказы по дням (с фильтром по дате)", "Orders by day (date filter)", "Commandes par jour (filtre par date)", "الطلبات حسب اليوم (مع تصفية بالتاريخ)");

        // ===== Message boxes =====
        Add("Выберите запись", "Select a record", "Sélectionnez un enregistrement", "اختر سجلاً");
        Add("Изменение", "Edit", "Modification", "تعديل");
        Add("Удаление", "Deletion", "Suppression", "حذف");
        Add("Удалить запись №{0} из «{1}»?", "Delete record #{0} from “{1}”?", "Supprimer l'enregistrement n°{0} de « {1} » ?", "هل تريد حذف السجل رقم {0} من «{1}»؟");
        Add("Подтверждение", "Confirmation", "Confirmation", "تأكيد");
        Add("Сначала откройте таблицу из меню «Справочники» или «Документы».", "First open a table from the “Directories” or “Documents” menu.", "Ouvrez d'abord une table depuis le menu « Référentiels » ou « Documents ».", "افتح أولاً جدولاً من قائمة «الجداول المرجعية» أو «المستندات».");
        Add("Экспорт", "Export", "Exportation", "تصدير");
        Add("Ошибка экспорта", "Export error", "Erreur d'exportation", "خطأ في التصدير");
        Add("Введите запрос", "Enter a query", "Saisissez une requête", "أدخل استعلاماً");
        Add("Разрешён один запрос SELECT.", "Only a single SELECT query is allowed.", "Une seule requête SELECT est autorisée.", "يُسمح باستعلام SELECT واحد فقط.");
        Add("Запрос отклонён", "Query rejected", "Requête refusée", "تم رفض الاستعلام");
        Add("Ошибка запроса", "Query error", "Erreur de requête", "خطأ في الاستعلام");

        // ===== Order page =====
        Add("Заказ", "Order", "Commande", "الطلب");
        Add("Выберите пациента и лекарство из списков.", "Select a patient and a medicine from the lists.", "Sélectionnez un patient et un médicament dans les listes.", "اختر المريض والدواء من القوائم.");
        Add("Введите количество — целое число больше нуля (например, 5).", "Enter the quantity as a whole number greater than zero (for example, 5).", "Saisissez la quantité sous forme de nombre entier supérieur à zéro (par exemple 5).", "أدخل الكمية كعدد صحيح أكبر من الصفر (مثلاً 5).");
        Add("Количество должно быть больше нуля.", "The quantity must be greater than zero.", "La quantité doit être supérieure à zéro.", "يجب أن تكون الكمية أكبر من الصفر.");
        Add("Это число слишком велико. Введите количество не больше 2 147 483 647.", "This number is too large. Enter a quantity of no more than 2,147,483,647.", "Ce nombre est trop grand. Saisissez une quantité d'au plus 2 147 483 647.", "هذا الرقم كبير جداً. أدخل كمية لا تزيد عن 2,147,483,647.");
        Add("Недостаточно лекарства на складе", "Not enough medicine in stock", "Stock de médicament insuffisant", "لا توجد كمية كافية من الدواء في المخزون");
        Add("Лекарства «{0}» сейчас нет на складе (остаток: 0).\n\nЧто делать: выберите другое лекарство или сначала оформите поставку этого лекарства.",
            "“{0}” is out of stock right now (stock: 0).\n\nWhat to do: choose another medicine or register a supply of this medicine first.",
            "« {0} » est actuellement en rupture de stock (stock : 0).\n\nQue faire : choisissez un autre médicament ou enregistrez d'abord une livraison de ce médicament.",
            "الدواء «{0}» غير متوفر في المخزون حالياً (المتوفر: 0).\n\nما العمل: اختر دواءً آخر أو سجّل أولاً توريداً لهذا الدواء.");
        Add("Недостаточно лекарства «{0}» на складе: вы запросили {1}, а в наличии только {2}.\n\nЧто делать: уменьшите количество до {2} или меньше, либо сначала оформите поставку этого лекарства.",
            "Not enough “{0}” in stock: you requested {1}, but only {2} are available.\n\nWhat to do: reduce the quantity to {2} or less, or register a supply of this medicine first.",
            "Stock insuffisant pour « {0} » : vous avez demandé {1}, mais seulement {2} sont disponibles.\n\nQue faire : réduisez la quantité à {2} ou moins, ou enregistrez d'abord une livraison de ce médicament.",
            "لا توجد كمية كافية من «{0}» في المخزون: طلبت {1} بينما المتوفر {2} فقط.\n\nما العمل: قلّل الكمية إلى {2} أو أقل، أو سجّل أولاً توريداً لهذا الدواء.");
        Add("Заказ успешно создан!", "Order created successfully!", "Commande créée avec succès !", "تم إنشاء الطلب بنجاح!");

        // ===== Edit window =====
        Add("Добавление: {0}", "Add: {0}", "Ajout : {0}", "إضافة: {0}");
        Add("Изменение: {0}", "Edit: {0}", "Modification : {0}", "تعديل: {0}");
        Add("Сохранить", "Save", "Enregistrer", "حفظ");
        Add("Отмена", "Cancel", "Annuler", "إلغاء");
        Add("Запись не найдена", "Record not found", "Enregistrement introuvable", "السجل غير موجود");
        Add("Заполните поле «{0}»", "Fill in the “{0}” field", "Renseignez le champ « {0} »", "املأ الحقل «{0}»");
        Add("Поле «{0}» должно быть целым числом", "The “{0}” field must be a whole number", "Le champ « {0} » doit être un nombre entier", "يجب أن يكون الحقل «{0}» عدداً صحيحاً");
        Add("Поле «{0}» должно быть числом", "The “{0}” field must be a number", "Le champ « {0} » doit être un nombre", "يجب أن يكون الحقل «{0}» رقماً");
        Add("Поле «{0}»: время в формате ЧЧ:ММ", "The “{0}” field: enter the time as HH:MM", "Champ « {0} » : saisissez l'heure au format HH:MM", "الحقل «{0}»: أدخل الوقت بصيغة HH:MM");
        Add("Поле «{0}»: число слишком большое (максимум 2 147 483 647)", "The “{0}” field: the number is too large (maximum 2,147,483,647)", "Champ « {0} » : nombre trop grand (maximum 2 147 483 647)", "الحقل «{0}»: الرقم كبير جداً (الحد الأقصى 2,147,483,647)");
        Add("Проверьте данные", "Check your input", "Vérifiez les données", "تحقق من البيانات");

        // ===== Login =====
        Add("Вход в систему", "Sign in", "Connexion", "تسجيل الدخول");
        Add("Логин", "Username", "Identifiant", "اسم المستخدم");
        Add("Пароль", "Password", "Mot de passe", "كلمة المرور");
        Add("Войти", "Sign in", "Se connecter", "دخول");
        Add("Войти как гость", "Continue as guest", "Continuer en tant qu'invité", "الدخول كزائر");
        Add("Введите логин и пароль", "Enter your username and password", "Saisissez l'identifiant et le mot de passe", "أدخل اسم المستخدم وكلمة المرور");
        Add("Неверный логин или пароль", "Incorrect username or password", "Identifiant ou mot de passe incorrect", "اسم المستخدم أو كلمة المرور غير صحيحة");
        Add("Язык интерфейса", "Interface language", "Langue de l'interface", "لغة الواجهة");

        // ===== Settings =====
        Add("Тема оформления", "Theme", "Thème", "السمة");
        Add("Classic (классический 3D)", "Classic (3D)", "Classic (3D classique)", "كلاسيكي (ثلاثي الأبعاد)");
        Add("Light (светлая)", "Light", "Light (clair)", "فاتح");
        Add("Dark (тёмная)", "Dark", "Dark (sombre)", "داكن");
        Add("Blue (синяя)", "Blue", "Blue (bleu)", "أزرق");
        Add("Размер шрифта", "Font size", "Taille de police", "حجم الخط");

        // ===== Change password =====
        Add("Смена пароля", "Change password", "Changement de mot de passe", "تغيير كلمة المرور");
        Add("Старый пароль", "Old password", "Ancien mot de passe", "كلمة المرور القديمة");
        Add("Новый пароль", "New password", "Nouveau mot de passe", "كلمة المرور الجديدة");
        Add("Повторите новый пароль", "Repeat new password", "Répétez le nouveau mot de passe", "أعد إدخال كلمة المرور الجديدة");
        Add("Заполните все поля", "Fill in all fields", "Remplissez tous les champs", "املأ جميع الحقول");
        Add("Новый пароль ≥ 4 символов", "The new password must be at least 4 characters", "Le nouveau mot de passe doit comporter au moins 4 caractères", "يجب ألا تقل كلمة المرور الجديدة عن 4 أحرف");
        Add("Новые пароли не совпадают", "The new passwords do not match", "Les nouveaux mots de passe ne correspondent pas", "كلمتا المرور الجديدتان غير متطابقتين");
        Add("Новый пароль совпадает со старым", "The new password is the same as the old one", "Le nouveau mot de passe est identique à l'ancien", "كلمة المرور الجديدة مطابقة للقديمة");
        Add("Старый пароль неверен", "The old password is incorrect", "L'ancien mot de passe est incorrect", "كلمة المرور القديمة غير صحيحة");
        Add("Пароль изменён", "Password changed", "Mot de passe modifié", "تم تغيير كلمة المرور");
        Add("Готово", "Done", "Terminé", "تم");

        // ===== Help window =====
        Add("Версия 1.0", "Version 1.0", "Version 1.0", "الإصدار 1.0");
        Add("Курсовой проект по дисциплине «Базы данных»", "Course project for the “Databases” subject", "Projet de cours de la discipline « Bases de données »", "مشروع مقرر «قواعد البيانات»");
        Add("Вариант 3. Разработка фрагмента информационной системы Аптека", "Variant 3. Development of a fragment of a Pharmacy information system", "Variante 3. Développement d'un fragment du système d'information d'une pharmacie", "الخيار 3. تطوير جزء من نظام معلومات صيدلية");
        Add("Стек технологий:", "Technology stack:", "Technologies utilisées :", "التقنيات المستخدمة:");
        Add("Технологии: параметризованные запросы, SHA-256, роль-ориентированное меню", "Techniques: parameterized queries, SHA-256, role-based menu", "Techniques : requêtes paramétrées, SHA-256, menu selon le rôle", "الأساليب: استعلامات ذات معاملات، SHA-256، قائمة حسب الدور");

        Add("1. Вход в систему", "1. Signing in", "1. Connexion", "1. تسجيل الدخول");
        Add("При запуске открывается окно входа. Введите логин и пароль, выданные администратором, или нажмите «Войти как гость» для просмотра каталога без права редактирования. Язык интерфейса можно выбрать прямо в окне входа.",
            "The sign-in window opens at startup. Enter the username and password given by the administrator, or click “Continue as guest” to browse the catalog without editing rights. You can pick the interface language right in the sign-in window.",
            "La fenêtre de connexion s'ouvre au démarrage. Saisissez l'identifiant et le mot de passe fournis par l'administrateur, ou cliquez sur « Continuer en tant qu'invité » pour consulter le catalogue sans droit de modification. La langue de l'interface se choisit directement dans cette fenêtre.",
            "تُفتح نافذة تسجيل الدخول عند التشغيل. أدخل اسم المستخدم وكلمة المرور اللذين يحددهما المدير، أو اضغط «الدخول كزائر» لتصفح الكتالوج دون صلاحية التعديل. يمكنك اختيار لغة الواجهة من نافذة تسجيل الدخول نفسها.");
        Add("Тестовые учётные записи: admin / admin123 (Администратор), pharm / pharm123 (Фармацевт).",
            "Test accounts: admin / admin123 (Administrator), pharm / pharm123 (Pharmacist).",
            "Comptes de test : admin / admin123 (Administrateur), pharm / pharm123 (Pharmacien).",
            "حسابات تجريبية: admin / admin123 (المدير)، pharm / pharm123 (الصيدلي).");
        Add("Пароли хранятся в виде SHA-256-хеша, поэтому восстановить исходный пароль нельзя — его можно только сменить.",
            "Passwords are stored as SHA-256 hashes, so the original password cannot be recovered — it can only be changed.",
            "Les mots de passe sont stockés sous forme de hachage SHA-256 : le mot de passe d'origine ne peut pas être retrouvé, seulement modifié.",
            "تُخزَّن كلمات المرور على شكل بصمة SHA-256، لذلك لا يمكن استرجاع كلمة المرور الأصلية — يمكن فقط تغييرها.");
        Add("2. Роли и права", "2. Roles and permissions", "2. Rôles et droits", "2. الأدوار والصلاحيات");
        Add("Администратор — полный доступ ко всем таблицам, ручной ввод SQL в отчётах, аналитика.",
            "Administrator — full access to all tables, manual SQL input in reports, analytics.",
            "Administrateur — accès complet à toutes les tables, saisie manuelle de SQL dans les rapports, analyses.",
            "المدير — وصول كامل إلى جميع الجداول، وإدخال SQL يدوياً في التقارير، والتحليلات.");
        Add("Фармацевт — CRUD по пациентам, заказам, рецептам; чтение остального; аналитика.",
            "Pharmacist — create/read/update/delete for patients, orders and prescriptions; read-only for the rest; analytics.",
            "Pharmacien — création/lecture/modification/suppression pour les patients, commandes et ordonnances ; lecture seule pour le reste ; analyses.",
            "الصيدلي — إضافة وعرض وتعديل وحذف للمرضى والطلبات والوصفات؛ قراءة فقط لبقية الجداول؛ التحليلات.");
        Add("Гость — только чтение каталога лекарств и связанных справочников; аналитика.",
            "Guest — read-only access to the medicine catalog and related directories; analytics.",
            "Invité — lecture seule du catalogue des médicaments et des référentiels associés ; analyses.",
            "الزائر — قراءة فقط لكتالوج الأدوية والجداول المرجعية المرتبطة به؛ التحليلات.");
        Add("3. Справочники", "3. Directories", "3. Référentiels", "3. الجداول المرجعية");
        Add("Меню «Справочники» содержит словарные таблицы, а подменю «Связующие таблицы (M:N)» — таблицы связей. При выборе таблицы открывается страница с таблицей данных:",
            "The “Directories” menu contains the dictionary tables and the “Link tables (M:N)” submenu contains the relationship tables. Choosing a table opens a page with the data grid:",
            "Le menu « Référentiels » contient les tables de dictionnaire et le sous-menu « Tables de liaison (M:N) » les tables de relations. Le choix d'une table ouvre une page avec la grille de données :",
            "تحتوي قائمة «الجداول المرجعية» على جداول القواميس، وتحتوي القائمة الفرعية «جداول الربط (M:N)» على جداول العلاقات. عند اختيار جدول تُفتح صفحة تعرض بياناته:");
        Add("«Добавить» — форма добавления с проверкой обязательных полей и значениями по умолчанию.",
            "“Add” — an entry form that checks required fields and fills in default values.",
            "« Ajouter » — formulaire d'ajout avec vérification des champs obligatoires et valeurs par défaut.",
            "«إضافة» — نموذج إضافة يتحقق من الحقول الإلزامية ويعبّئ القيم الافتراضية.");
        Add("«Изменить» — открывает выбранную запись (или двойной щелчок по строке).",
            "“Edit” — opens the selected record (or double-click the row).",
            "« Modifier » — ouvre l'enregistrement sélectionné (ou double-clic sur la ligne).",
            "«تعديل» — يفتح السجل المحدد (أو انقر نقراً مزدوجاً على الصف).");
        Add("«Удалить» — удаление с подтверждением; если запись используется в других таблицах, программа объяснит, что нужно сделать.",
            "“Delete” — deletion with confirmation; if the record is used in other tables, the program explains what to do.",
            "« Supprimer » — suppression avec confirmation ; si l'enregistrement est utilisé dans d'autres tables, le programme explique quoi faire.",
            "«حذف» — الحذف بعد التأكيد؛ وإذا كان السجل مستخدماً في جداول أخرى يشرح البرنامج ما يجب فعله.");
        Add("Поле «Поиск» фильтрует данные по всем столбцам одновременно.",
            "The “Search” box filters the data across all columns at once.",
            "Le champ « Recherche » filtre les données sur toutes les colonnes à la fois.",
            "يقوم حقل «بحث» بتصفية البيانات في جميع الأعمدة في وقت واحد.");
        Add("Столбцы можно сортировать щелчком по заголовку.",
            "Columns can be sorted by clicking the header.",
            "Les colonnes se trient en cliquant sur leur en-tête.",
            "يمكن ترتيب الأعمدة بالنقر على العنوان.");
        Add("Внешние ключи показаны как читаемые названия (ФИО, название лекарства и т. д.).",
            "Foreign keys are shown as readable names (full name, medicine name, etc.).",
            "Les clés étrangères sont affichées sous forme de noms lisibles (nom complet, nom du médicament, etc.).",
            "تُعرض المفاتيح الخارجية بأسماء مقروءة (الاسم الكامل، اسم الدواء، إلخ).");
        Add("4. Документы", "4. Documents", "4. Documents", "4. المستندات");
        Add("«Оформление заказа» — выберите пациента и лекарство, укажите количество и нажмите «Оформить заказ». Если на складе меньше лекарства, чем вы запросили, программа покажет, сколько его в наличии.",
            "“Place an order” — choose a patient and a medicine, enter the quantity and click “Place order”. If the stock is lower than the quantity you requested, the program shows how much is available.",
            "« Passer une commande » — choisissez un patient et un médicament, saisissez la quantité et cliquez sur « Valider la commande ». Si le stock est inférieur à la quantité demandée, le programme indique la quantité disponible.",
            "«إنشاء طلب» — اختر المريض والدواء وأدخل الكمية ثم اضغط «تأكيد الطلب». إذا كان المخزون أقل من الكمية المطلوبة فسيعرض البرنامج الكمية المتوفرة.");
        Add("«Журналы документов» — заказы, рецепты, заявки и поставки.",
            "“Document logs” — orders, prescriptions, requests and supplies.",
            "« Journaux de documents » — commandes, ordonnances, demandes et livraisons.",
            "«سجلات المستندات» — الطلبات والوصفات وطلبات التوريد والتوريدات.");
        Add("«Отчёты и запросы» — 20 шаблонов; ручной ввод SELECT доступен только администратору.",
            "“Reports and queries” — 20 templates; manual SELECT input is available to the administrator only.",
            "« Rapports et requêtes » — 20 modèles ; la saisie manuelle de SELECT est réservée à l'administrateur.",
            "«التقارير والاستعلامات» — 20 قالباً؛ إدخال SELECT يدوياً متاح للمدير فقط.");
        Add("«Экспорт текущей таблицы в CSV» — сохраняет открытую таблицу (разделитель «;», UTF-8 BOM).",
            "“Export current table to CSV” — saves the open table (separator “;”, UTF-8 BOM).",
            "« Exporter la table actuelle en CSV » — enregistre la table ouverte (séparateur « ; », UTF-8 BOM).",
            "«تصدير الجدول الحالي إلى CSV» — يحفظ الجدول المفتوح (الفاصل «;»، ترميز UTF-8 BOM).");
        Add("5. Аналитика", "5. Analytics", "5. Analyses", "5. التحليلات");
        Add("Раздел показывает дашборд из четырёх диаграмм: столбчатые диаграммы (количество лекарств и средняя цена по категориям), круговая диаграмма (заказы по статусам) и линейный график (заказы по дням). Фильтр по датам действует на график «Заказы по дням». Можно включить автообновление (каждые 5 секунд) и экспортировать весь дашборд в PNG.",
            "This section shows a dashboard of four charts: bar charts (medicines count and average price by category), a pie chart (orders by status) and a line graph (orders by day). The date filter applies to the “Orders by day” graph. You can turn on auto-refresh (every 5 seconds) and export the whole dashboard to PNG.",
            "Cette section affiche un tableau de bord de quatre graphiques : histogrammes (nombre de médicaments et prix moyen par catégorie), diagramme circulaire (commandes par statut) et courbe (commandes par jour). Le filtre de dates s'applique à la courbe « Commandes par jour ». Vous pouvez activer l'actualisation automatique (toutes les 5 secondes) et exporter tout le tableau de bord en PNG.",
            "يعرض هذا القسم لوحة معلومات من أربعة مخططات: مخططان شريطيان (عدد الأدوية ومتوسط السعر حسب الفئة) ومخطط دائري (الطلبات حسب الحالة) ومخطط خطي (الطلبات حسب اليوم). تُطبَّق تصفية التاريخ على مخطط «الطلبات حسب اليوم». يمكنك تفعيل التحديث التلقائي (كل 5 ثوانٍ) وتصدير اللوحة كاملة إلى PNG.");
        Add("6. Справка и Разное", "6. Help and Misc", "6. Aide et Divers", "6. المساعدة وأخرى");
        Add("«Справка → Руководство пользователя» — этот текст.",
            "“Help → User guide” — this text.",
            "« Aide → Guide de l'utilisateur » — ce texte.",
            "«المساعدة ← دليل المستخدم» — هذا النص.");
        Add("«Справка → О программе» — информация о версии.",
            "“Help → About” — version information.",
            "« Aide → À propos » — informations sur la version.",
            "«المساعدة ← حول البرنامج» — معلومات الإصدار.");
        Add("«Разное → Настройка» — тема (Classic 3D / Light / Dark / Blue), размер шрифта и язык интерфейса (русский, English, Français, العربية). Сохраняется в %AppData%\\Apteka\\settings.json.",
            "“Misc → Settings” — theme (Classic 3D / Light / Dark / Blue), font size and interface language (Russian, English, French, Arabic). Saved in %AppData%\\Apteka\\settings.json.",
            "« Divers → Paramètres » — thème (Classic 3D / Light / Dark / Blue), taille de police et langue de l'interface (russe, anglais, français, arabe). Enregistré dans %AppData%\\Apteka\\settings.json.",
            "«أخرى ← الإعدادات» — السمة (Classic 3D / Light / Dark / Blue) وحجم الخط ولغة الواجهة (الروسية، الإنجليزية، الفرنسية، العربية). تُحفظ في %AppData%\\Apteka\\settings.json.");
        Add("«Разное → Сменить пароль» — старый пароль проверяется по хешу, новый должен быть ≥ 4 символов.",
            "“Misc → Change password” — the old password is verified by its hash; the new one must be at least 4 characters.",
            "« Divers → Changer le mot de passe » — l'ancien mot de passe est vérifié par son hachage ; le nouveau doit comporter au moins 4 caractères.",
            "«أخرى ← تغيير كلمة المرور» — يتم التحقق من كلمة المرور القديمة عبر البصمة؛ ويجب ألا تقل الجديدة عن 4 أحرف.");
        Add("7. Журнал действий", "7. Action log", "7. Journal des actions", "7. سجل الإجراءات");
        Add("Все входы, изменения данных, ошибки и экспорт записываются в таблицу ActionLog (на русском языке).",
            "All sign-ins, data changes, errors and exports are written to the ActionLog table (in Russian).",
            "Toutes les connexions, modifications de données, erreurs et exportations sont enregistrées dans la table ActionLog (en russe).",
            "تُسجَّل جميع عمليات الدخول وتغييرات البيانات والأخطاء وعمليات التصدير في جدول ActionLog (باللغة الروسية).");
        Add("8. Если что-то пошло не так", "8. If something goes wrong", "8. En cas de problème", "8. إذا حدث خطأ ما");
        Add("Вместо технических сообщений базы данных программа показывает понятное объяснение: что произошло и что нужно сделать (например, «Недостаточно лекарства на складе»). Для непредвиденных ошибок внизу сообщения приводятся технические подробности — покажите их администратору.",
            "Instead of technical database messages the program shows a clear explanation: what happened and what to do (for example, “Not enough medicine in stock”). For unexpected errors the technical details are shown at the bottom of the message — show them to the administrator.",
            "Au lieu des messages techniques de la base de données, le programme affiche une explication claire : ce qui s'est passé et ce qu'il faut faire (par exemple « Stock de médicament insuffisant »). Pour les erreurs imprévues, les détails techniques figurent en bas du message — montrez-les à l'administrateur.",
            "بدلاً من رسائل قاعدة البيانات التقنية يعرض البرنامج شرحاً واضحاً: ما الذي حدث وما العمل (مثلاً «لا توجد كمية كافية من الدواء في المخزون»). وفي الأخطاء غير المتوقعة تظهر التفاصيل التقنية أسفل الرسالة — أظهرها للمدير.");

        // ===== Table titles =====
        Add("Города", "Cities", "Villes", "المدن");
        Add("Улицы", "Streets", "Rues", "الشوارع");
        Add("Должности", "Positions", "Postes", "الوظائف");
        Add("Диагнозы", "Diagnoses", "Diagnostics", "التشخيصات");
        Add("Технологии приготовления", "Preparation technologies", "Technologies de préparation", "تقنيات التحضير");
        Add("Вещества", "Substances", "Substances", "المواد");
        Add("Компоненты", "Components", "Composants", "المكونات");
        Add("Лекарства", "Medicines", "Médicaments", "الأدوية");
        Add("Готовые лекарства", "Ready-made medicines", "Médicaments prêts à l'emploi", "الأدوية الجاهزة");
        Add("Изготовляемые лекарства", "Compounded medicines", "Médicaments magistraux", "الأدوية المركّبة");
        Add("Пациенты", "Patients", "Patients", "المرضى");
        Add("Врачи", "Doctors", "Médecins", "الأطباء");
        Add("Производители", "Manufacturers", "Fabricants", "المصنّعون");
        Add("Заказы", "Orders", "Commandes", "الطلبات");
        Add("Состав заказов", "Order items", "Contenu des commandes", "محتويات الطلبات");
        Add("Рецепты", "Prescriptions", "Ordonnances", "الوصفات الطبية");
        Add("Состав рецепта", "Prescription items", "Contenu des ordonnances", "محتويات الوصفات");
        Add("Заявки на вещества", "Substance requests", "Demandes de substances", "طلبات توريد المواد");
        Add("Заявки на компоненты", "Component requests", "Demandes de composants", "طلبات توريد المكونات");
        Add("Поставки", "Supplies", "Livraisons", "التوريدات");
        Add("Состав готового (M:N)", "Ready-medicine composition (M:N)", "Composition des médicaments prêts (M:N)", "تركيب الدواء الجاهز (M:N)");
        Add("Состав изготовляемого (M:N)", "Compounded-medicine composition (M:N)", "Composition des médicaments magistraux (M:N)", "تركيب الدواء المركّب (M:N)");
        Add("Диагнозы рецепта (M:N)", "Prescription diagnoses (M:N)", "Diagnostics de l'ordonnance (M:N)", "تشخيصات الوصفة (M:N)");
        Add("Диагнозы пациента (M:N)", "Patient diagnoses (M:N)", "Diagnostics du patient (M:N)", "تشخيصات المريض (M:N)");
        Add("Рецепты заказа (M:N)", "Order prescriptions (M:N)", "Ordonnances de la commande (M:N)", "وصفات الطلب (M:N)");
        Add("Заявки в-в по заказу (M:N)", "Order substance requests (M:N)", "Demandes de substances par commande (M:N)", "طلبات توريد المواد حسب الطلب (M:N)");
        Add("Заявки комп. по заказу (M:N)", "Order component requests (M:N)", "Demandes de composants par commande (M:N)", "طلبات توريد المكونات حسب الطلب (M:N)");

        // ===== Column / field names =====
        Add("Город", "City", "Ville", "المدينة");
        Add("Улица", "Street", "Rue", "الشارع");
        Add("Должность", "Position", "Poste", "الوظيفة");
        Add("Название", "Name", "Nom", "الاسم");
        Add("Врач", "Doctor", "Médecin", "الطبيب");
        Add("Технология", "Technology", "Technologie", "التقنية");
        Add("Способ приготовления", "Preparation method", "Méthode de préparation", "طريقة التحضير");
        Add("Вещество", "Substance", "Substance", "المادة");
        Add("Описание", "Description", "Description", "الوصف");
        Add("Остаток", "Stock", "Stock", "المخزون");
        Add("Компонент", "Component", "Composant", "المكوّن");
        Add("Критическая норма", "Critical level", "Seuil critique", "الحد الحرج");
        Add("Тип", "Type", "Type", "النوع");
        Add("Дозировка", "Dosage", "Dosage", "الجرعة");
        Add("Форма выпуска", "Release form", "Forme galénique", "الشكل الدوائي");
        Add("Срок текстом", "Shelf life (text)", "Durée de conservation (texte)", "مدة الصلاحية (نص)");
        Add("Дата производства", "Manufacture date", "Date de fabrication", "تاريخ الإنتاج");
        Add("Дней годности", "Shelf life (days)", "Durée de conservation (jours)", "مدة الصلاحية (أيام)");
        Add("Годен до", "Expiry date", "Date de péremption", "صالح حتى");
        Add("Цена", "Price", "Prix", "السعر");
        Add("Изготовляемое", "Compounded medicine", "Médicament magistral", "الدواء المركّب");
        Add("Способ применения", "Usage method", "Mode d'emploi", "طريقة الاستخدام");
        Add("Время изготовления", "Preparation time", "Temps de préparation", "وقت التحضير");
        Add("Фамилия", "Last name", "Nom de famille", "اسم العائلة");
        Add("Имя", "First name", "Prénom", "الاسم الأول");
        Add("Отчество", "Middle name", "Deuxième prénom", "اسم الأب");
        Add("Возраст", "Age", "Âge", "العمر");
        Add("Дом", "House no.", "N° de maison", "رقم المنزل");
        Add("Дата заказа", "Order date", "Date de commande", "تاريخ الطلب");
        Add("Телефон", "Phone", "Téléphone", "الهاتف");
        Add("Статус", "Status", "Statut", "الحالة");
        Add("Номер рецепта", "Prescription number", "Numéro d'ordonnance", "رقم الوصفة");
        Add("Дата выписки", "Issue date", "Date d'émission", "تاريخ الإصدار");
        Add("Рецепт", "Prescription", "Ordonnance", "الوصفة");
        Add("Дата заявки", "Request date", "Date de la demande", "تاريخ طلب التوريد");
        Add("Дата поставки", "Supply date", "Date de livraison", "تاريخ التوريد");
        Add("Производитель", "Manufacturer", "Fabricant", "المصنّع");
        Add("Диагноз", "Diagnosis", "Diagnostic", "التشخيص");
        Add("Заявка", "Request", "Demande", "طلب توريد");

        // ===== Report templates =====
        Add("1. Все данные из таблицы лекарств", "1. All data from the medicines table", "1. Toutes les données de la table des médicaments", "1. جميع بيانات جدول الأدوية");
        Add("2. Заказчики: «А» в фамилии и возраст старше 40", "2. Customers: last name contains “А” and age over 40", "2. Clients : nom contenant « А » et âge supérieur à 40 ans", "2. العملاء: اسم العائلة يحتوي «А» والعمر فوق 40");
        Add("3. Цена 100-500 или название «Аналь...» / «...зол»", "3. Price 100–500 or name “Аналь…” / “…зол”", "3. Prix 100–500 ou nom « Аналь… » / « …зол »", "3. السعر 100–500 أو الاسم «Аналь…» / «…зол»");
        Add("4. Заказы с информацией о лекарствах и покупателях", "4. Orders with medicine and customer details", "4. Commandes avec détails des médicaments et des clients", "4. الطلبات مع معلومات الأدوية والعملاء");
        Add("5. Технологии и лекарства (LEFT JOIN)", "5. Technologies and medicines (LEFT JOIN)", "5. Technologies et médicaments (LEFT JOIN)", "5. التقنيات والأدوية (LEFT JOIN)");
        Add("6. ФИО врачей одним столбцом", "6. Doctors' full names in one column", "6. Noms complets des médecins dans une colonne", "6. الأسماء الكاملة للأطباء في عمود واحد");
        Add("7. Лекарства дешевле средней цены", "7. Medicines cheaper than the average price", "7. Médicaments moins chers que le prix moyen", "7. أدوية أرخص من متوسط السعر");
        Add("8. Статистика по медикаментам", "8. Medicine statistics", "8. Statistiques des médicaments", "8. إحصاءات الأدوية");
        Add("9. Категории > 10 наименований", "9. Categories with more than 10 items", "9. Catégories de plus de 10 articles", "9. الفئات التي تضم أكثر من 10 أصناف");
        Add("10. Уникальные диагнозы", "10. Unique diagnoses", "10. Diagnostics uniques", "10. التشخيصات الفريدة");
        Add("11. Заказы: дата ↓, стоимость ↑", "11. Orders: date ↓, cost ↑", "11. Commandes : date ↓, coût ↑", "11. الطلبات: التاريخ ↓، التكلفة ↑");
        Add("12. Топ-5 самых дорогих со 2-го места", "12. Top 5 most expensive, starting from 2nd place", "12. Top 5 des plus chers à partir de la 2e place", "12. أغلى 5 أدوية بدءاً من المركز الثاني");
        Add("13. Покупатели с > 3 заказов", "13. Customers with more than 3 orders", "13. Clients avec plus de 3 commandes", "13. العملاء الذين لديهم أكثر من 3 طلبات");
        Add("14. Топ-3 самых частых лекарств", "14. Top 3 most frequently ordered medicines", "14. Top 3 des médicaments les plus commandés", "14. أكثر 3 أدوية طلباً");
        Add("15. Лекарства с остатком < 10", "15. Medicines with stock below 10", "15. Médicaments avec un stock inférieur à 10", "15. أدوية مخزونها أقل من 10");
        Add("16. Компоненты, используемые < 5 раз", "16. Components used fewer than 5 times", "16. Composants utilisés moins de 5 fois", "16. المكونات المستخدمة أقل من 5 مرات");
        Add("17. Категоризация цены (CASE)", "17. Price categorization (CASE)", "17. Catégorisation des prix (CASE)", "17. تصنيف الأسعار (CASE)");
        Add("18. Заказы в производстве / в обработке", "18. Orders in production / processing", "18. Commandes en production / en traitement", "18. الطلبات قيد التصنيع / المعالجة");
        Add("19. Статистика по категориям", "19. Statistics by category", "19. Statistiques par catégorie", "19. الإحصاءات حسب الفئة");
        Add("20. Лекарства с макс. и мин. сроком годности", "20. Medicines with max and min shelf life", "20. Médicaments à durée de conservation max. et min.", "20. الأدوية ذات أطول وأقصر مدة صلاحية");

        // ===== Friendly database errors (see DbErrors.cs) =====
        Add("Ошибка", "Error", "Erreur", "خطأ");
        Add("Ошибка базы данных", "Database error", "Erreur de base de données", "خطأ في قاعدة البيانات");
        Add("Операция не выполнена", "Operation failed", "Opération non effectuée", "لم تتم العملية");
        Add("Операция невозможна", "Operation not possible", "Opération impossible", "العملية غير ممكنة");
        Add("Сервер отвечает слишком долго", "The server is taking too long to respond", "Le serveur met trop de temps à répondre", "الخادم يستغرق وقتاً طويلاً في الاستجابة");
        Add("База данных не ответила вовремя.\n\nЧто делать: подождите немного и повторите попытку. Если ошибка повторяется — сообщите администратору.",
            "The database did not respond in time.\n\nWhat to do: wait a moment and try again. If the error keeps happening, tell the administrator.",
            "La base de données n'a pas répondu à temps.\n\nQue faire : patientez un instant et réessayez. Si l'erreur persiste, informez l'administrateur.",
            "لم تستجب قاعدة البيانات في الوقت المناسب.\n\nما العمل: انتظر قليلاً ثم أعد المحاولة. وإذا تكرر الخطأ فأبلغ المدير.");
        Add("Произошла непредвиденная ошибка.\n\nЧто делать: повторите попытку. Если ошибка повторяется — покажите администратору текст ниже.\n\nТехнические подробности:\n{0}",
            "An unexpected error occurred.\n\nWhat to do: try again. If the error keeps happening, show the text below to the administrator.\n\nTechnical details:\n{0}",
            "Une erreur inattendue s'est produite.\n\nQue faire : réessayez. Si l'erreur persiste, montrez le texte ci-dessous à l'administrateur.\n\nDétails techniques :\n{0}",
            "حدث خطأ غير متوقع.\n\nما العمل: أعد المحاولة. وإذا تكرر الخطأ فأظهر النص أدناه للمدير.\n\nالتفاصيل التقنية:\n{0}");
        Add("Произошла непредвиденная ошибка (код {0}).\n\nЧто делать: повторите попытку. Если ошибка повторяется — покажите администратору текст ниже.\n\nТехнические подробности:\n{1}",
            "An unexpected error occurred (code {0}).\n\nWhat to do: try again. If the error keeps happening, show the text below to the administrator.\n\nTechnical details:\n{1}",
            "Une erreur inattendue s'est produite (code {0}).\n\nQue faire : réessayez. Si l'erreur persiste, montrez le texte ci-dessous à l'administrateur.\n\nDétails techniques :\n{1}",
            "حدث خطأ غير متوقع (الرمز {0}).\n\nما العمل: أعد المحاولة. وإذا تكرر الخطأ فأظهر النص أدناه للمدير.\n\nالتفاصيل التقنية:\n{1}");
        Add("Остаток лекарства на складе не может стать меньше нуля.\n\nЧто делать: если вы оформляете заказ — уменьшите количество или сначала пополните склад (оформите поставку). Если вы меняете остаток вручную — введите число 0 или больше.",
            "The stock of the medicine cannot drop below zero.\n\nWhat to do: if you are placing an order, reduce the quantity or restock first (register a supply). If you are editing the stock by hand, enter 0 or more.",
            "Le stock du médicament ne peut pas devenir inférieur à zéro.\n\nQue faire : si vous passez une commande, réduisez la quantité ou réapprovisionnez d'abord (enregistrez une livraison). Si vous modifiez le stock à la main, saisissez 0 ou plus.",
            "لا يمكن أن ينخفض مخزون الدواء إلى أقل من صفر.\n\nما العمل: إذا كنت تنشئ طلباً فقلّل الكمية أو أعد التزويد أولاً (سجّل توريداً). وإذا كنت تعدّل المخزون يدوياً فأدخل 0 أو أكثر.");
        Add("Недопустимое значение", "Invalid value", "Valeur non valide", "قيمة غير مقبولة");
        Add("Значение в поле «{0}» не допускается правилами базы данных (например, отрицательное число или значение вне допустимого диапазона).\n\nЧто делать: исправьте значение и повторите попытку.",
            "The value in the “{0}” field is not allowed by the database rules (for example, a negative number or a value outside the allowed range).\n\nWhat to do: correct the value and try again.",
            "La valeur du champ « {0} » n'est pas autorisée par les règles de la base de données (par exemple un nombre négatif ou une valeur hors plage).\n\nQue faire : corrigez la valeur et réessayez.",
            "القيمة في الحقل «{0}» غير مسموح بها وفق قواعد قاعدة البيانات (مثلاً رقم سالب أو قيمة خارج النطاق المسموح).\n\nما العمل: صحّح القيمة وأعد المحاولة.");
        Add("Одно из введённых значений не допускается правилами базы данных.\n\nЧто делать: проверьте введённые данные (числа, даты, количество) и повторите попытку.",
            "One of the entered values is not allowed by the database rules.\n\nWhat to do: check the data you entered (numbers, dates, quantity) and try again.",
            "L'une des valeurs saisies n'est pas autorisée par les règles de la base de données.\n\nQue faire : vérifiez les données saisies (nombres, dates, quantité) et réessayez.",
            "إحدى القيم المدخلة غير مسموح بها وفق قواعد قاعدة البيانات.\n\nما العمل: تحقق من البيانات المدخلة (الأرقام والتواريخ والكمية) وأعد المحاولة.");
        Add("Запись нельзя удалить", "The record cannot be deleted", "L'enregistrement ne peut pas être supprimé", "لا يمكن حذف السجل");
        Add("Эта запись используется в другом месте — в таблице «{0}».\n\nЧто делать: сначала удалите или измените связанные записи, затем повторите удаление.",
            "This record is used elsewhere — in the “{0}” table.\n\nWhat to do: first delete or change the related records, then try deleting again.",
            "Cet enregistrement est utilisé ailleurs — dans la table « {0} ».\n\nQue faire : supprimez ou modifiez d'abord les enregistrements liés, puis réessayez.",
            "هذا السجل مستخدم في مكان آخر — في الجدول «{0}».\n\nما العمل: احذف السجلات المرتبطة به أو عدّلها أولاً ثم أعد محاولة الحذف.");
        Add("Запись нельзя удалить: на неё ссылаются другие таблицы.\n\nЧто делать: сначала удалите или измените связанные записи, затем повторите удаление.",
            "The record cannot be deleted: other tables refer to it.\n\nWhat to do: first delete or change the related records, then try deleting again.",
            "L'enregistrement ne peut pas être supprimé : d'autres tables y font référence.\n\nQue faire : supprimez ou modifiez d'abord les enregistrements liés, puis réessayez.",
            "لا يمكن حذف السجل: توجد جداول أخرى تشير إليه.\n\nما العمل: احذف السجلات المرتبطة به أو عدّلها أولاً ثم أعد محاولة الحذف.");
        Add("Связанная запись не найдена", "Related record not found", "Enregistrement lié introuvable", "السجل المرتبط غير موجود");
        Add("Одно из выбранных значений (из списка) больше не существует в базе — возможно, его удалили.\n\nЧто делать: закройте окно, обновите данные и выберите значение заново.",
            "One of the values chosen from a list no longer exists in the database — it may have been deleted.\n\nWhat to do: close the window, refresh the data and choose the value again.",
            "L'une des valeurs choisies dans une liste n'existe plus dans la base — elle a peut-être été supprimée.\n\nQue faire : fermez la fenêtre, actualisez les données et choisissez à nouveau la valeur.",
            "إحدى القيم المختارة من القائمة لم تعد موجودة في قاعدة البيانات — ربما تم حذفها.\n\nما العمل: أغلق النافذة وحدّث البيانات ثم اختر القيمة من جديد.");
        Add("Данные нарушают правило целостности базы данных.\n\nЧто делать: проверьте введённые значения и повторите попытку.",
            "The data violates a database integrity rule.\n\nWhat to do: check the entered values and try again.",
            "Les données enfreignent une règle d'intégrité de la base.\n\nQue faire : vérifiez les valeurs saisies et réessayez.",
            "تخالف البيانات إحدى قواعد سلامة قاعدة البيانات.\n\nما العمل: تحقق من القيم المدخلة وأعد المحاولة.");
        Add("Такая запись уже существует", "This record already exists", "Cet enregistrement existe déjà", "هذا السجل موجود بالفعل");
        Add("В базе уже есть запись с таким значением, а повторы не допускаются.", "The database already has a record with this value, and duplicates are not allowed.", "La base contient déjà un enregistrement avec cette valeur, et les doublons ne sont pas autorisés.", "توجد في قاعدة البيانات سجل بهذه القيمة بالفعل، والتكرار غير مسموح.");
        Add("Повторяющееся значение: {0}", "Duplicate value: {0}", "Valeur en double : {0}", "القيمة المكررة: {0}");
        Add("Что делать: введите другое значение или откройте существующую запись.", "What to do: enter a different value or open the existing record.", "Que faire : saisissez une autre valeur ou ouvrez l'enregistrement existant.", "ما العمل: أدخل قيمة مختلفة أو افتح السجل الموجود.");
        Add("Не заполнено обязательное поле", "A required field is empty", "Champ obligatoire non renseigné", "حقل إلزامي فارغ");
        Add("Поле «{0}» обязательно для заполнения.\n\nЧто делать: заполните его и повторите попытку.",
            "The “{0}” field is required.\n\nWhat to do: fill it in and try again.",
            "Le champ « {0} » est obligatoire.\n\nQue faire : renseignez-le et réessayez.",
            "الحقل «{0}» إلزامي.\n\nما العمل: املأه وأعد المحاولة.");
        Add("Одно из обязательных полей не заполнено.\n\nЧто делать: заполните все поля, отмеченные звёздочкой, и повторите попытку.",
            "One of the required fields is empty.\n\nWhat to do: fill in all fields marked with an asterisk and try again.",
            "L'un des champs obligatoires n'est pas renseigné.\n\nQue faire : renseignez tous les champs marqués d'un astérisque et réessayez.",
            "أحد الحقول الإلزامية فارغ.\n\nما العمل: املأ جميع الحقول المميزة بنجمة وأعد المحاولة.");
        Add("Слишком длинное значение", "The value is too long", "Valeur trop longue", "القيمة طويلة جداً");
        Add("Текст в поле «{0}» слишком длинный и не помещается в базу данных.\n\nЧто делать: сократите текст и повторите попытку.",
            "The text in the “{0}” field is too long and does not fit in the database.\n\nWhat to do: shorten the text and try again.",
            "Le texte du champ « {0} » est trop long et ne tient pas dans la base de données.\n\nQue faire : raccourcissez le texte et réessayez.",
            "النص في الحقل «{0}» طويل جداً ولا يتسع له حقل قاعدة البيانات.\n\nما العمل: اختصر النص وأعد المحاولة.");
        Add("Один из введённых текстов слишком длинный и не помещается в базу данных.\n\nЧто делать: сократите текст и повторите попытку.",
            "One of the entered texts is too long and does not fit in the database.\n\nWhat to do: shorten the text and try again.",
            "L'un des textes saisis est trop long et ne tient pas dans la base de données.\n\nQue faire : raccourcissez le texte et réessayez.",
            "أحد النصوص المدخلة طويل جداً ولا يتسع له حقل قاعدة البيانات.\n\nما العمل: اختصر النص وأعد المحاولة.");
        Add("Конфликт с другим пользователем", "Conflict with another user", "Conflit avec un autre utilisateur", "تعارض مع مستخدم آخر");
        Add("Операция отменена: другой пользователь одновременно менял те же данные.\n\nЧто делать: повторите действие.",
            "The operation was cancelled: another user was changing the same data at the same time.\n\nWhat to do: repeat the action.",
            "L'opération a été annulée : un autre utilisateur modifiait les mêmes données en même temps.\n\nQue faire : répétez l'action.",
            "أُلغيت العملية: كان مستخدم آخر يعدّل البيانات نفسها في الوقت نفسه.\n\nما العمل: كرّر الإجراء.");
        Add("База данных недоступна", "The database is unavailable", "Base de données inaccessible", "قاعدة البيانات غير متاحة");
        Add("База данных «apteka» не найдена или у вас нет к ней доступа.\n\nЧто делать: создайте базу и выполните скрипт apteka_app_setup.sql либо обратитесь к администратору.",
            "The “apteka” database was not found or you do not have access to it.\n\nWhat to do: create the database and run the apteka_app_setup.sql script, or contact the administrator.",
            "La base de données « apteka » est introuvable ou vous n'y avez pas accès.\n\nQue faire : créez la base et exécutez le script apteka_app_setup.sql, ou contactez l'administrateur.",
            "قاعدة البيانات «apteka» غير موجودة أو لا تملك صلاحية الوصول إليها.\n\nما العمل: أنشئ القاعدة ونفّذ السكربت apteka_app_setup.sql أو تواصل مع المدير.");
        Add("Вход на сервер отклонён", "Server sign-in rejected", "Connexion au serveur refusée", "رُفض الدخول إلى الخادم");
        Add("SQL Server не принял учётные данные.\n\nЧто делать: проверьте права вашей учётной записи Windows на сервере или данные в строке подключения (файл Db.cs).",
            "SQL Server did not accept the credentials.\n\nWhat to do: check your Windows account's permissions on the server or the connection string (Db.cs).",
            "SQL Server n'a pas accepté les identifiants.\n\nQue faire : vérifiez les droits de votre compte Windows sur le serveur ou la chaîne de connexion (fichier Db.cs).",
            "لم يقبل SQL Server بيانات الدخول.\n\nما العمل: تحقق من صلاحيات حساب Windows الخاص بك على الخادم أو من سلسلة الاتصال (الملف Db.cs).");
        Add("Таблица не найдена", "Table not found", "Table introuvable", "الجدول غير موجود");
        Add("Таблица или представление «{0}» не найдены в базе данных.\n\nЧто делать: проверьте название в запросе. Если это служебный объект приложения — выполните скрипт apteka_app_setup.sql.",
            "The table or view “{0}” was not found in the database.\n\nWhat to do: check the name in the query. If it is a service object of the application, run the apteka_app_setup.sql script.",
            "La table ou la vue « {0} » est introuvable dans la base de données.\n\nQue faire : vérifiez le nom dans la requête. S'il s'agit d'un objet de service de l'application, exécutez le script apteka_app_setup.sql.",
            "الجدول أو العرض «{0}» غير موجود في قاعدة البيانات.\n\nما العمل: تحقق من الاسم في الاستعلام. وإذا كان كائناً خدمياً للتطبيق فنفّذ السكربت apteka_app_setup.sql.");
        Add("Столбец не найден", "Column not found", "Colonne introuvable", "العمود غير موجود");
        Add("Столбец «{0}» не найден.\n\nЧто делать: проверьте название столбца в запросе.",
            "The column “{0}” was not found.\n\nWhat to do: check the column name in the query.",
            "La colonne « {0} » est introuvable.\n\nQue faire : vérifiez le nom de la colonne dans la requête.",
            "العمود «{0}» غير موجود.\n\nما العمل: تحقق من اسم العمود في الاستعلام.");
        Add("Процедура не найдена", "Procedure not found", "Procédure introuvable", "الإجراء المخزن غير موجود");
        Add("Хранимая процедура «{0}» не найдена в базе.\n\nЧто делать: выполните скрипт apteka_app_setup.sql в SSMS.",
            "The stored procedure “{0}” was not found in the database.\n\nWhat to do: run the apteka_app_setup.sql script in SSMS.",
            "La procédure stockée « {0} » est introuvable dans la base.\n\nQue faire : exécutez le script apteka_app_setup.sql dans SSMS.",
            "الإجراء المخزن «{0}» غير موجود في قاعدة البيانات.\n\nما العمل: نفّذ السكربت apteka_app_setup.sql في SSMS.");
        Add("Нет связи с базой данных", "Cannot reach the database", "Pas de connexion à la base de données", "لا يوجد اتصال بقاعدة البيانات");
        Add("Не удалось подключиться к серверу базы данных.\n\nЧто делать: убедитесь, что SQL Server (или LocalDB) запущен и компьютер подключён к сети; при необходимости проверьте строку подключения в файле Db.cs.",
            "Could not connect to the database server.\n\nWhat to do: make sure SQL Server (or LocalDB) is running and the computer is connected to the network; if needed, check the connection string in Db.cs.",
            "Impossible de se connecter au serveur de base de données.\n\nQue faire : vérifiez que SQL Server (ou LocalDB) est démarré et que l'ordinateur est connecté au réseau ; si besoin, vérifiez la chaîne de connexion dans Db.cs.",
            "تعذّر الاتصال بخادم قاعدة البيانات.\n\nما العمل: تأكد من أن SQL Server (أو LocalDB) يعمل وأن الحاسوب متصل بالشبكة؛ وعند الحاجة تحقق من سلسلة الاتصال في الملف Db.cs.");
        Add("Недопустимое число", "Invalid number", "Nombre non valide", "رقم غير مقبول");
        Add("Введённое число слишком большое или имеет неподходящий формат.\n\nЧто делать: введите меньшее значение и повторите попытку.",
            "The number entered is too large or has the wrong format.\n\nWhat to do: enter a smaller value and try again.",
            "Le nombre saisi est trop grand ou son format est incorrect.\n\nQue faire : saisissez une valeur plus petite et réessayez.",
            "الرقم المدخل كبير جداً أو صيغته غير صحيحة.\n\nما العمل: أدخل قيمة أصغر وأعد المحاولة.");
        Add("Недопустимая дата", "Invalid date", "Date non valide", "تاريخ غير مقبول");
        Add("Дата указана неверно или выходит за допустимый диапазон.\n\nЧто делать: выберите дату в календаре и повторите попытку.",
            "The date is incorrect or outside the allowed range.\n\nWhat to do: pick a date in the calendar and try again.",
            "La date est incorrecte ou hors de la plage autorisée.\n\nQue faire : choisissez une date dans le calendrier et réessayez.",
            "التاريخ غير صحيح أو خارج النطاق المسموح.\n\nما العمل: اختر تاريخاً من التقويم وأعد المحاولة.");
        Add("Ошибка в SQL-запросе", "Error in the SQL query", "Erreur dans la requête SQL", "خطأ في استعلام SQL");
        Add("В тексте запроса есть синтаксическая ошибка рядом с «{0}».\n\nЧто делать: проверьте запрос (скобки, запятые, ключевые слова) и выполните его снова.",
            "There is a syntax error in the query near “{0}”.\n\nWhat to do: check the query (brackets, commas, keywords) and run it again.",
            "Il y a une erreur de syntaxe dans la requête près de « {0} ».\n\nQue faire : vérifiez la requête (parenthèses, virgules, mots-clés) et exécutez-la à nouveau.",
            "يوجد خطأ في صياغة الاستعلام قرب «{0}».\n\nما العمل: تحقق من الاستعلام (الأقواس والفواصل والكلمات المفتاحية) ونفّذه مرة أخرى.");
        Add("Недостаточно прав", "Insufficient permissions", "Droits insuffisants", "صلاحيات غير كافية");
        Add("У вас нет прав на это действие в базе данных.\n\nЧто делать: обратитесь к администратору.",
            "You do not have permission for this action in the database.\n\nWhat to do: contact the administrator.",
            "Vous n'avez pas le droit d'effectuer cette action dans la base de données.\n\nQue faire : contactez l'administrateur.",
            "ليست لديك صلاحية لتنفيذ هذا الإجراء في قاعدة البيانات.\n\nما العمل: تواصل مع المدير.");
    }
}

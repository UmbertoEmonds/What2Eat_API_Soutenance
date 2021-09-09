
#az account set -s $subscription # ...or use 'az login'
az login


# Création de la base de données
location="East US"
randomIdentifier="what2eat"

resource="$randomIdentifier-resource"
server="$randomIdentifier-server"
database="$randomIdentifier-database"

login="antoine"
password="beta_AZURE"

startIP=0.0.0.0
endIP=255.0.0.0
echo -e "\E[32m Using resource group $resource with login: $login, password: $password \E[0m"

echo -e "\E[32m Creating $resource \E[0m"
az group create --name $resource --location "$location"

echo -e "\E[32m Creating $server Azure Database  on $resource \E[0m"
az mysql server create --resource-group $resource --name $server --location "$location" --admin-user $login --admin-password $password --sku-name B_Gen5_1 --version 5.7

echo -e "\E[32m Configuring firewall \E[0m"
az mysql server firewall-rule create --resource-group $resource --server $server --name AllowMyIP --start-ip-address $startIP --end-ip-address $endIP

echo -e "\E[32m SHOW $database on $server \E[0m"
az mysql server show --resource-group $resource --name $server

echo -e "\E[32m create shéma in $database on $server \E[0m"
mysql -h what2eat-server.mysql.database.azure.com -u antoine@what2eat-server -p < createAzureDatabase.sql

echo -e "\E[32m import data in $database on $server \E[0m"
mysql -h what2eat-server.mysql.database.azure.com -u antoine@what2eat-server -p < data.sql

$SHELL



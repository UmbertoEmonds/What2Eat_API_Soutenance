echo "delete what2eat"
az login 

az group delete --name "what2eat-resource" --yes

$SHELL
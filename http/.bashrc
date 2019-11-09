DOTENVDIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"
DOTENVFILE=$DOTENVDIR/.env
while read p; do
    export $p
done < "$DOTENVFILE"

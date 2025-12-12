import requests
import re

API_URL = "https://api.fbi.gov/wanted/v1/list"


def extract_reward_value(text):
    if not text:
        return 0
    # Find tal i teksten
    match = re.search(r"(\d[\d,.]*)", text)
    if match:
        return int(match.group(1).replace(",", ""))
    return 0


def fetch_and_sort():
    response = requests.get(API_URL)
    response.raise_for_status()
    data = response.json()

    # Sortér efter reward (størst først)
    sorted_items = sorted(
        data["items"],
        key=lambda x: extract_reward_value(x.get("reward_text")),
        reverse=True
    )

    return sorted_items[:10]  # top 10


def main():
    print("🔍 Henter & sorterer FBI Top 10…\n")

    items = fetch_and_sort()

    for i, item in enumerate(items, start=1):
        title = item.get("title", "Ukendt")
        reward = item.get("reward_text", "Ingen belønning")
        print(f"{i}. {title} — {reward}")


if __name__ == "__main__":
    main()

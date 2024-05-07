'use client'

export default function ToMainPageBtn() {
    return (
        <div>
            <button
                className="p-3 m-1 text-white bg-fuchsia-500 rounded-xl font-semibold text-xl hoover:bg-fuchsia-600 button-press"
                onClick={e => {
                    window.location.href = "http://localhost:3000"
                }}
            >
                To main page
            </button>
        </div>
    )
}
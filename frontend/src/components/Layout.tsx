import React from 'react';
import { Link, useLocation } from 'react-router-dom';

interface LayoutProps {
  children: React.ReactNode;
}

export const Layout: React.FC<LayoutProps> = ({ children }) => {
  const location = useLocation();

  const navItemClass = (isActive: boolean): string =>
    `rounded-xl px-4 py-2 text-sm font-semibold transition-all duration-200 ${
      isActive
        ? 'bg-slate-800 text-white shadow-sm'
        : 'text-slate-600 hover:bg-slate-100 hover:text-slate-900'
    }`;

  return (
    <div className="min-h-screen bg-slate-100/60">
      <header className="sticky top-0 z-20 border-b border-slate-200 bg-white/90 backdrop-blur">
        <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
          <div className="flex items-center justify-between py-4 sm:py-5">
            <div className="flex items-center">
              <h1 className="text-xl font-semibold tracking-tight text-slate-900 sm:text-2xl">
                AI Readiness Assessment Portal
              </h1>
            </div>
            <nav className="flex items-center gap-2 rounded-2xl border border-slate-200 bg-slate-50/80 p-1 shadow-sm">
              <Link
                to="/"
                className={navItemClass(location.pathname === '/')}
              >
                Dashboard
              </Link>
              <Link
                to="/organizations"
                className={navItemClass(location.pathname === '/organizations')}
              >
                Organizations
              </Link>
            </nav>
          </div>
        </div>
      </header>

      <main className="mx-auto max-w-7xl px-4 py-8 sm:px-6 sm:py-10 lg:px-8 xl:px-10">
        {children}
      </main>
    </div>
  );
};